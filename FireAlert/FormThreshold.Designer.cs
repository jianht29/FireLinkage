namespace FireAlert
{
    partial class FormThreshold
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiTrackBarSmoke = new Sunny.UI.UITrackBar();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiSymbolButtonOk = new Sunny.UI.UISymbolButton();
            this.uiSymbolButtonCancel = new Sunny.UI.UISymbolButton();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiTextBoxSmoke = new Sunny.UI.UITextBox();
            this.uiTrackBarFire = new Sunny.UI.UITrackBar();
            this.uiTextBoxFire = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // uiTrackBarSmoke
            // 
            this.uiTrackBarSmoke.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTrackBarSmoke.Location = new System.Drawing.Point(132, 54);
            this.uiTrackBarSmoke.Maximum = 4096;
            this.uiTrackBarSmoke.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTrackBarSmoke.Name = "uiTrackBarSmoke";
            this.uiTrackBarSmoke.Size = new System.Drawing.Size(249, 29);
            this.uiTrackBarSmoke.TabIndex = 0;
            this.uiTrackBarSmoke.Text = "烟雾报警阈值";
            this.uiTrackBarSmoke.Value = 2000;
            this.uiTrackBarSmoke.ValueChanged += new System.EventHandler(this.uiTrackBarSmoke_ValueChanged);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(17, 57);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(125, 23);
            this.uiLabel1.TabIndex = 3;
            this.uiLabel1.Text = "烟雾报警阈值：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiSymbolButtonOk
            // 
            this.uiSymbolButtonOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uiSymbolButtonOk.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButtonOk.Location = new System.Drawing.Point(225, 165);
            this.uiSymbolButtonOk.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButtonOk.Name = "uiSymbolButtonOk";
            this.uiSymbolButtonOk.Size = new System.Drawing.Size(110, 35);
            this.uiSymbolButtonOk.Symbol = 61528;
            this.uiSymbolButtonOk.TabIndex = 4;
            this.uiSymbolButtonOk.Text = "确　定";
            this.uiSymbolButtonOk.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButtonOk.Click += new System.EventHandler(this.uiSymbolButtonOk_Click);
            // 
            // uiSymbolButtonCancel
            // 
            this.uiSymbolButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiSymbolButtonCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButtonCancel.Location = new System.Drawing.Point(350, 165);
            this.uiSymbolButtonCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButtonCancel.Name = "uiSymbolButtonCancel";
            this.uiSymbolButtonCancel.Size = new System.Drawing.Size(110, 35);
            this.uiSymbolButtonCancel.Symbol = 61527;
            this.uiSymbolButtonCancel.TabIndex = 5;
            this.uiSymbolButtonCancel.Text = "取　消";
            this.uiSymbolButtonCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButtonCancel.Click += new System.EventHandler(this.uiSymbolButtonCancel_Click);
            // 
            // uiLine1
            // 
            this.uiLine1.BackColor = System.Drawing.Color.Transparent;
            this.uiLine1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLine1.Location = new System.Drawing.Point(3, 134);
            this.uiLine1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(477, 29);
            this.uiLine1.TabIndex = 6;
            // 
            // uiTextBoxSmoke
            // 
            this.uiTextBoxSmoke.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBoxSmoke.DoubleValue = 2000D;
            this.uiTextBoxSmoke.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBoxSmoke.IntValue = 2000;
            this.uiTextBoxSmoke.Location = new System.Drawing.Point(388, 54);
            this.uiTextBoxSmoke.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBoxSmoke.Maximum = 4096D;
            this.uiTextBoxSmoke.Minimum = 0D;
            this.uiTextBoxSmoke.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBoxSmoke.Name = "uiTextBoxSmoke";
            this.uiTextBoxSmoke.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBoxSmoke.ShowText = false;
            this.uiTextBoxSmoke.Size = new System.Drawing.Size(72, 29);
            this.uiTextBoxSmoke.TabIndex = 7;
            this.uiTextBoxSmoke.Text = "2000";
            this.uiTextBoxSmoke.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTextBoxSmoke.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBoxSmoke.Watermark = "";
            this.uiTextBoxSmoke.TextChanged += new System.EventHandler(this.uiTextBoxSmoke_TextChanged);
            // 
            // uiTrackBarFire
            // 
            this.uiTrackBarFire.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTrackBarFire.Location = new System.Drawing.Point(132, 97);
            this.uiTrackBarFire.Maximum = 4096;
            this.uiTrackBarFire.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTrackBarFire.Name = "uiTrackBarFire";
            this.uiTrackBarFire.Size = new System.Drawing.Size(249, 29);
            this.uiTrackBarFire.TabIndex = 8;
            this.uiTrackBarFire.Text = "烟雾报警阈值";
            this.uiTrackBarFire.Value = 2000;
            this.uiTrackBarFire.ValueChanged += new System.EventHandler(this.uiTrackBarFire_ValueChanged);
            // 
            // uiTextBoxFire
            // 
            this.uiTextBoxFire.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBoxFire.DoubleValue = 2000D;
            this.uiTextBoxFire.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBoxFire.IntValue = 2000;
            this.uiTextBoxFire.Location = new System.Drawing.Point(388, 97);
            this.uiTextBoxFire.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBoxFire.Maximum = 4096D;
            this.uiTextBoxFire.Minimum = 0D;
            this.uiTextBoxFire.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBoxFire.Name = "uiTextBoxFire";
            this.uiTextBoxFire.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBoxFire.ShowText = false;
            this.uiTextBoxFire.Size = new System.Drawing.Size(72, 29);
            this.uiTextBoxFire.TabIndex = 10;
            this.uiTextBoxFire.Text = "2000";
            this.uiTextBoxFire.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiTextBoxFire.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.uiTextBoxFire.Watermark = "";
            this.uiTextBoxFire.TextChanged += new System.EventHandler(this.uiTextBoxFire_TextChanged);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(17, 100);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(125, 23);
            this.uiLabel2.TabIndex = 9;
            this.uiLabel2.Text = "火焰报警阈值：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormThreshold
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(482, 217);
            this.Controls.Add(this.uiTrackBarFire);
            this.Controls.Add(this.uiTextBoxFire);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiTrackBarSmoke);
            this.Controls.Add(this.uiTextBoxSmoke);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.uiSymbolButtonCancel);
            this.Controls.Add(this.uiSymbolButtonOk);
            this.Controls.Add(this.uiLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormThreshold";
            this.Text = "报警阈值设置";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.Load += new System.EventHandler(this.FormThreshold_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITrackBar uiTrackBarSmoke;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UISymbolButton uiSymbolButtonOk;
        private Sunny.UI.UISymbolButton uiSymbolButtonCancel;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UITextBox uiTextBoxSmoke;
        private Sunny.UI.UITrackBar uiTrackBarFire;
        private Sunny.UI.UITextBox uiTextBoxFire;
        private Sunny.UI.UILabel uiLabel2;
    }
}