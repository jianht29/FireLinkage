/*
使用MQTTnet和SunnyUI开发的《火警联动》物联网教学项目的大屏幕可视化演示工具。

1.MQTTnet是一个高性能的.NET库。用于连接MQTT服务器，订阅主题和向主题发布消息。

FireAlert文件夹下为可视化程序的源代码
FireLinkage文件夹下为使用Mind+和mPython设计的基于掌控板的火警探测终端的图形化程序。

MQTTnet相关的资源链接：

GitHub：https://github.com/dotnet/MQTTnet
NuGet：https://www.nuget.org/packages/MQTTnet/

2.SunnyUI.NET是基于.NET框架的C# WinForm开源控件库、工具类库、扩展类库、多页面开发框架。用于简化可视化用户界面的设计。 

SunnyUI相关的网络链接：

帮助文档：https://gitee.com/yhuse/SunnyUI/wikis/pages
Gitee：https://gitee.com/yhuse/SunnyUI
GitHub：https://github.com/yhuse/SunnyUI
Nuget：https://www.nuget.org/packages/SunnyUI/

3.SIoT是一个为教育定制的跨平台的开源MQTT服务器程序。建议使用SIoT1.3作为MQTT服务器进行本示例程序的测试。

SIoT相关的网络链接：

使用手册：https://siot.readthedocs.io/zh-cn/latest/
Gitee：https://gitee.com/vvlink/SIoT
GitHub：https://github.com/vvlink/SIoT/
*/
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Sunny.UI;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace FireAlert
{
    public partial class FormMain : UIForm
    {
        public static IMqttClient MqttClient;
        /// <summary>
        /// MQTT服务器的IP地址或域名
        /// </summary>
        public string Mqtt_Server = "127.0.0.1";
        /// <summary>
        /// MQTT服务器的端口，一般为1883
        /// </summary>
        public int Mqtt_Port = 1883;
        /// <summary>
        /// MQTT服务器的用户名
        /// </summary>
        public string Mqtt_UserName = "siot";
        /// <summary>
        /// MQTT服务器的密码
        /// </summary>
        public string Mqtt_PassWord = "dfrobot";
        /// <summary>
        /// MQTT客户端的ID
        /// </summary>
        public string Mqtt_ClientId = "FireAlertCtrl_";

        // 主题风格全局变量
        public UIStyle UiStyle = UIStyle.LayuiGreen;
        // 烟雾传感器和火焰传感器折线图数据索引
        public int indexSmoke = 0;
        public int indexFire = 0;

        /// <summary>
        /// 使用指定的参数连接MQTT服务器
        /// </summary>
        public void MqttClientStart()
        {
            // MQTT连接参数
            var mqttClientOptionsBuilder = new MqttClientOptionsBuilder()
                .WithTcpServer(Mqtt_Server, Mqtt_Port)	// MQTT服务器的IP和端口号
                .WithCredentials(Mqtt_UserName, Mqtt_PassWord)      // MQTT服务器的用户名和密码
                .WithClientId(Mqtt_ClientId + Guid.NewGuid().ToString("N"))	// 自动设置客户端ID的后缀，以免出现重复
                .WithCleanSession();

            var mqttClientOptions = mqttClientOptionsBuilder.Build();
            MqttClient = new MqttFactory().CreateMqttClient();

            // 客户端连接成功事件
            MqttClient.ConnectedAsync += MqttClient_ConnectedAsync;
            // 客户端连接关闭事件
            MqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            // 收到订阅消息事件
            MqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
            // 连接MQTT服务器
            try
            {
                MqttClient.ConnectAsync(mqttClientOptions);
            }
            catch (Exception ex)
            {
                this.Text = "火警联动实时监测信息系统  - " + ex.Message;
            }
        }

        /// <summary>
        /// 断开已经连接的MQTT服务器
        /// </summary>
        public void MqttClientStop()
        {
            if (MqttClient != null && MqttClient.IsConnected)
            {
                var mqttClientDisconnectOptions = new MqttFactory().CreateClientDisconnectOptionsBuilder().Build();
                MqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
                MqttClient.Dispose();
                MqttClient = null;
            }
        }

        /// <summary>
        /// 当MQTT服务器连接被断开时发生的事件
        /// </summary>
        private Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = "火警联动实时监测信息系统 - 没有连接到MQTT服务器，请先连接MQTT服务器";
            }));
            return Task.CompletedTask;
        }

        /// <summary>
        /// 当MQTT服务器成功连接时发生的事件
        /// </summary>
        private Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = "火警联动实时监测信息系统 - 连接到MQTT服务器";
            }));
            // 订阅消息主题
            // MqttQualityOfServiceLevel: （QoS）:
            // AtMostOnce 0: 最多一次，接收者不确认收到消息，并且消息不被发送者存储和重新发送提供与底层 TCP 协议相同的保证。
            // AtLeastOnce 1: 保证一条消息至少有一次会传递给接收方。发送方存储消息，直到它从接收方收到确认收到消息的数据包。一条消息可以多次发送或传递。
            // ExactlyOnce 2: 保证每条消息仅由预期的收件人接收一次。级别2是最安全和最慢的服务质量级别，保证由发送方和接收方之间的至少两个请求/响应（四次握手）。
            MqttClient.SubscribeAsync("FireLinkage/AlertPlace", MqttQualityOfServiceLevel.AtLeastOnce);
            MqttClient.SubscribeAsync("FireLinkage/Smoke1", MqttQualityOfServiceLevel.AtLeastOnce);
            MqttClient.SubscribeAsync("FireLinkage/Fire1", MqttQualityOfServiceLevel.AtLeastOnce);
            MqttClient.SubscribeAsync("FireLinkage/Smoke2", MqttQualityOfServiceLevel.AtLeastOnce);
            MqttClient.SubscribeAsync("FireLinkage/Fire2", MqttQualityOfServiceLevel.AtLeastOnce);
            MqttClient.SubscribeAsync("FireLinkage/Smoke3", MqttQualityOfServiceLevel.AtLeastOnce);
            MqttClient.SubscribeAsync("FireLinkage/Fire3", MqttQualityOfServiceLevel.AtLeastOnce);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 当从MQTT服务器收到订阅消息的事件
        /// </summary>
        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            this.Invoke(new Action(() =>
            {
                double value = 0;
                string place = "";

                switch (arg.ApplicationMessage.Topic)
                {
                    case "FireLinkage/AlertPlace":
                        place = arg.ApplicationMessage.ConvertPayloadToString();

                        if (place.Trim() == "A")
                        {
                            uiLedBulb1.On = true;
                            uiLedBulb1.Blink = true;
                        }
                        if (place.Trim() == "B")
                        {
                            uiLedBulb2.On = true;
                            uiLedBulb2.Blink = true;
                        }
                        if (place.Trim() == "C")
                        {
                            uiLedBulb3.On = true;
                            uiLedBulb3.Blink = true;
                        }

                        timer1.Start();

                        string placeColor = "";

                        switch (place.Trim())
                        {
                            case "A":
                                placeColor = "点位A（蓝）";
                                break;
                            case "B":
                                placeColor = "点位B（红）";
                                break;
                            case "C":
                                placeColor = "点位C（绿）";
                                break;
                            default:
                                break;
                        }

                        string[] data = { DateTime.Now.ToString(), placeColor };

                        this.uiDataGridViewPlace.AddRow(data);

                        this.uiDataGridViewPlace.Sort(uiDataGridViewPlace.Columns[0], ListSortDirection.Descending);

                        uiDataGridViewPlace.ClearSelection();
                        //uiDataGridViewPlace.CurrentCell = null;
                        //uiDataGridViewPlace.Rows[0].Selected = false;
                        break;
                    case "FireLinkage/Smoke1":
                        value = Convert.ToDouble(arg.ApplicationMessage.ConvertPayloadToString());
                        uiLedLabelSmoke1.Text = Convert.ToInt32(value).ToString();

                        uiProcessBarSmoke1.Value = Convert.ToInt32(value / 4096 * 100);
                        this.uiLineChartSmoke.Option.AddData("Smoke1", indexSmoke, value);
                        break;
                    case "FireLinkage/Fire1":
                        value = Convert.ToDouble(arg.ApplicationMessage.ConvertPayloadToString());
                        uiLedLabelFire1.Text = Convert.ToInt32(value).ToString();

                        uiProcessBarFire1.Value = Convert.ToInt32(value / 4096 * 100);
                        this.uiLineChartFire.Option.AddData("Fire1", indexSmoke, value);
                        break;
                    case "FireLinkage/Smoke2":
                        value = Convert.ToDouble(arg.ApplicationMessage.ConvertPayloadToString());
                        uiLedLabelSmoke2.Text = Convert.ToInt32(value).ToString();

                        uiProcessBarSmoke2.Value = Convert.ToInt32(value / 4096 * 100);
                        this.uiLineChartSmoke.Option.AddData("Smoke2", indexSmoke, value);
                        break;
                    case "FireLinkage/Fire2":
                        value = Convert.ToDouble(arg.ApplicationMessage.ConvertPayloadToString());
                        uiLedLabelFire2.Text = Convert.ToInt32(value).ToString();

                        uiProcessBarFire2.Value = Convert.ToInt32(value / 4096 * 100);
                        this.uiLineChartFire.Option.AddData("Fire2", indexSmoke, value);
                        break;
                    case "FireLinkage/Smoke3":
                        value = Convert.ToDouble(arg.ApplicationMessage.ConvertPayloadToString());
                        uiLedLabelSmoke3.Text = Convert.ToInt32(value).ToString();

                        uiProcessBarSmoke3.Value = Convert.ToInt32(value / 4096 * 100);
                        this.uiLineChartSmoke.Option.AddData("Smoke3", indexSmoke, value);
                        break;
                    case "FireLinkage/Fire3":
                        value = Convert.ToDouble(arg.ApplicationMessage.ConvertPayloadToString());
                        uiLedLabelFire3.Text = Convert.ToInt32(value).ToString();

                        uiProcessBarFire3.Value = Convert.ToInt32(value / 4096 * 100);
                        this.uiLineChartFire.Option.AddData("Fire3", indexSmoke, value);
                        break;
                    default:
                        break;
                }

                if (indexSmoke > 50)
                {
                    this.uiLineChartSmoke.Option.XAxis.SetRange(indexSmoke - 55, indexSmoke-5);
                }
                if (indexFire > 50)
                {
                    this.uiLineChartFire.Option.XAxis.SetRange(indexFire - 55, indexFire-5);
                }

                this.uiLineChartSmoke.Refresh();
                this.uiLineChartFire.Refresh();

                indexSmoke++;
                indexFire++;
            }));

            return Task.CompletedTask;
        }
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            indexSmoke = 0;
            indexFire = 0;

            //初始化烟雾传感器折线图
            UILineOption optionSmoke = new UILineOption();
            optionSmoke.ToolTip.Visible = true;
            optionSmoke.Title = new UITitle
            {
                Text = "烟雾传感器实时测量折线图"
                //SubText = "实时测量值"
            };
            var seriesSmoke1 = optionSmoke.AddSeries(new UILineSeries("Smoke1"));
            var seriesSmoke2 = optionSmoke.AddSeries(new UILineSeries("Smoke2"));
            var seriesSmoke3 = optionSmoke.AddSeries(new UILineSeries("Smoke3"));
            seriesSmoke1.CustomColor = true;
            seriesSmoke2.CustomColor = true;
            seriesSmoke3.CustomColor = true;
            seriesSmoke1.Color = UIColor.Blue;
            seriesSmoke2.Color = UIColor.Red;
            seriesSmoke3.Color = UIColor.Green;
            //设置曲线显示最大点数，超过后自动清理
            seriesSmoke1.SetMaxCount(56);
            seriesSmoke2.SetMaxCount(56);
            seriesSmoke3.SetMaxCount(56);
            //坐标轴显示小数位数
            optionSmoke.XAxis.AxisLabel.DecimalPlaces = 1;
            optionSmoke.YAxis.AxisLabel.DecimalPlaces = 1;
            this.uiLineChartSmoke.SetOption(optionSmoke);

            //初始化火焰传感器折线图
            UILineOption optionFire = new UILineOption();
            optionFire.ToolTip.Visible = true;
            optionFire.Title = new UITitle
            {
                Text = "火焰传感器实时测量折线图"
                //SubText = "实时测量值"
            };
            var seriesFire1 = optionFire.AddSeries(new UILineSeries("Fire1"));
            var seriesFire2 = optionFire.AddSeries(new UILineSeries("Fire2"));
            var seriesFire3 = optionFire.AddSeries(new UILineSeries("Fire3"));
            seriesFire1.CustomColor = true;
            seriesFire2.CustomColor = true;
            seriesFire3.CustomColor = true;
            seriesFire1.Color = UIColor.Blue;
            seriesFire2.Color = UIColor.Red;
            seriesFire3.Color = UIColor.Green;

            //设置曲线显示最大点数，超过后自动清理
            seriesFire1.SetMaxCount(56);
            seriesFire2.SetMaxCount(56);
            seriesFire3.SetMaxCount(56);

            //坐标轴显示小数位数
            optionFire.XAxis.AxisLabel.DecimalPlaces = 1;
            optionFire.YAxis.AxisLabel.DecimalPlaces = 1;
            this.uiLineChartFire.SetOption(optionFire);

            //设置默认的主题风格
            this.Style = UIStyle.LayuiGreen;
            this.uiTitlePanelSmoke.Style = UIStyle.LayuiGreen;
            this.uiTitlePanelFire.Style = UIStyle.LayuiGreen;
            this.uiTitlePanelAlert.Style = UIStyle.LayuiGreen;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.Default;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.Default;
            this.uiLineChartSmoke.Style = UIStyle.LayuiGreen;
            this.uiLineChartFire.Style = UIStyle.LayuiGreen;
            this.uiDataGridViewPlace.Style = UIStyle.LayuiGreen;
            this.uiContextMenuStrip1.Style = UIStyle.LayuiGreen;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.LayuiGreen;

            // 建议使用SIoT1.3作为MQTT服务器进行测试
            // 使用手册：https://siot.readthedocs.io/zh-cn/latest/
            // Gitee：https://gitee.com/vvlink/SIoT
            // GitHub：https://github.com/vvlink/SIoT/

            // 可以在任意位置重新设置需要连接的MQTT服务参数
            Mqtt_Server = "127.0.0.1";
            Mqtt_Port = 1883;
            Mqtt_UserName = "siot";
            Mqtt_PassWord = "dfrobot";
            Mqtt_ClientId = "MyClient_";

            // MQTTnet相关资源链接
            // GitHub：https://github.com/dotnet/MQTTnet
            // NuGet：https://www.nuget.org/packages/MQTTnet/

            // 使用MQTTnet连接MQTT服务器
            MqttClientStop();
            MqttClientStart();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            uiLedBulb1.Blink = false;
            uiLedBulb2.Blink = false;
            uiLedBulb3.Blink = false;
            uiLedBulb1.On = false;
            uiLedBulb2.On = false;
            uiLedBulb3.On = false;
            timer1.Stop();
        }
        private void toolStripMenuItemDark_Click(object sender, EventArgs e)
        {
            this.Style = UIStyle.Black;
            this.uiTitlePanelSmoke.Style = UIStyle.Black;
            this.uiTitlePanelFire.Style = UIStyle.Black;
            this.uiTitlePanelAlert.Style = UIStyle.Black;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.Dark;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.Dark;
            this.uiLineChartSmoke.Style = UIStyle.Black;
            this.uiLineChartFire.Style = UIStyle.Black;
            this.uiDataGridViewPlace.Style = UIStyle.Black;
            this.uiContextMenuStrip1.Style = UIStyle.Black;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.Black;
        }
        private void toolStripMenuItemLight_Click(object sender, EventArgs e)
        {
            this.Style = UIStyle.Inherited;
            this.uiTitlePanelSmoke.Style = UIStyle.Inherited;
            this.uiTitlePanelFire.Style = UIStyle.Inherited;
            this.uiTitlePanelAlert.Style = UIStyle.Inherited;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartSmoke.Style = UIStyle.Inherited;
            this.uiLineChartFire.Style = UIStyle.Inherited;
            this.uiDataGridViewPlace.Style = UIStyle.Inherited;
            this.uiContextMenuStrip1.Style = UIStyle.Inherited;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.Inherited;
        }
        private void toolStripMenuItemDefault_Click(object sender, EventArgs e)
        {
            this.Style = UIStyle.LayuiGreen;
            this.uiTitlePanelSmoke.Style = UIStyle.LayuiGreen;
            this.uiTitlePanelFire.Style = UIStyle.LayuiGreen;
            this.uiTitlePanelAlert.Style = UIStyle.LayuiGreen;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.Default;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.Default;
            this.uiLineChartSmoke.Style = UIStyle.LayuiGreen;
            this.uiLineChartFire.Style = UIStyle.LayuiGreen;
            this.uiDataGridViewPlace.Style = UIStyle.LayuiGreen;
            this.uiContextMenuStrip1.Style = UIStyle.LayuiGreen;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.LayuiGreen;
        }
        private void toolStripMenuItemBlue_Click(object sender, EventArgs e)
        {
            this.Style = UIStyle.DarkBlue;
            this.uiTitlePanelSmoke.Style = UIStyle.DarkBlue;
            this.uiTitlePanelFire.Style = UIStyle.DarkBlue;
            this.uiTitlePanelAlert.Style = UIStyle.DarkBlue;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.LiveChart;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.LiveChart;
            this.uiLineChartSmoke.Style = UIStyle.DarkBlue;
            this.uiLineChartFire.Style = UIStyle.DarkBlue;
            this.uiDataGridViewPlace.Style = UIStyle.DarkBlue;
            this.uiContextMenuStrip1.Style = UIStyle.DarkBlue;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.DarkBlue;
        }
        private void toolStripMenuItemPurple_Click(object sender, EventArgs e)
        {
            this.Style = UIStyle.Purple;
            this.uiTitlePanelSmoke.Style = UIStyle.Purple;
            this.uiTitlePanelFire.Style = UIStyle.Purple;
            this.uiTitlePanelAlert.Style = UIStyle.Purple;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartSmoke.Style = UIStyle.Purple;
            this.uiLineChartFire.Style = UIStyle.Purple;
            this.uiDataGridViewPlace.Style = UIStyle.Purple;
            this.uiContextMenuStrip1.Style = UIStyle.Purple;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.Purple;
        }
        private void toolStripMenuItemRed_Click(object sender, EventArgs e)
        {
            this.Style = UIStyle.Red;
            this.uiTitlePanelSmoke.Style = UIStyle.Red;
            this.uiTitlePanelFire.Style = UIStyle.Red;
            this.uiTitlePanelAlert.Style = UIStyle.Red;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartSmoke.Style = UIStyle.Red;
            this.uiLineChartFire.Style = UIStyle.Red;
            this.uiDataGridViewPlace.Style = UIStyle.Red;
            this.uiContextMenuStrip1.Style = UIStyle.Red;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.Red;
        }
        private void toolStripMenuItemOrange_Click(object sender, EventArgs e)
        {
            this.Style = UIStyle.Orange;
            this.uiTitlePanelSmoke.Style = UIStyle.Orange;
            this.uiTitlePanelFire.Style = UIStyle.Orange;
            this.uiTitlePanelAlert.Style = UIStyle.Orange;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartSmoke.Style = UIStyle.Orange;
            this.uiLineChartFire.Style = UIStyle.Orange;
            this.uiDataGridViewPlace.Style = UIStyle.Orange;
            this.uiContextMenuStrip1.Style = UIStyle.Orange;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.Orange;
        }
        private void toolStripMenuItemGreen_Click(object sender, EventArgs e)
        {
            this.Style = UIStyle.Green;
            this.uiTitlePanelSmoke.Style = UIStyle.Green;
            this.uiTitlePanelFire.Style = UIStyle.Green;
            this.uiTitlePanelAlert.Style = UIStyle.Green;
            this.uiLineChartSmoke.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartFire.ChartStyleType = UIChartStyleType.Plain;
            this.uiLineChartSmoke.Style = UIStyle.Green;
            this.uiLineChartFire.Style = UIStyle.Green;
            this.uiDataGridViewPlace.Style = UIStyle.Green;
            this.uiContextMenuStrip1.Style = UIStyle.Green;
            this.uiLineChartSmoke.Refresh();
            this.uiLineChartFire.Refresh();
            UiStyle = UIStyle.Green;
        }

        private void toolStripMenuItemMqtt_Click(object sender, EventArgs e)
        {
            FormMqtt formMqtt = new FormMqtt(this);
            formMqtt.ShowDialog();
        }
    }
}
