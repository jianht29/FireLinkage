using Sunny.UI;
using System;
using System.Windows.Forms;

namespace FireAlert
{
    public partial class FormThreshold : UIForm
    {
        public UIStyle UiStyle;
        public int AlertSmoke = 2000;
        public int AlertFire = 2000;
        public FormThreshold(UIStyle _uiStyle, int _alertSmoke, int _alertFire)
        {
            InitializeComponent();
            UiStyle = _uiStyle;
            AlertSmoke = _alertSmoke;
            AlertFire = _alertFire;
            this.uiTrackBarSmoke.MouseWheel += UiTrackBarTextBoxSmoke_MouseWheel;
            this.uiTextBoxSmoke.MouseWheel += UiTrackBarTextBoxSmoke_MouseWheel;
            this.uiTrackBarFire.MouseWheel += UiTrackBarTextBoxFire_MouseWheel;
            this.uiTextBoxFire.MouseWheel += UiTrackBarTextBoxFire_MouseWheel;
        }

        private void UiTrackBarTextBoxSmoke_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.uiTrackBarSmoke.Value++;
            }
            if (e.Delta < 0)
            {
                this.uiTrackBarSmoke.Value--;
            }
        }

        private void UiTrackBarTextBoxFire_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.uiTrackBarFire.Value++;
            }
            if (e.Delta < 0)
            {
                this.uiTrackBarFire.Value--;
            }
        }

        private void FormThreshold_Load(object sender, EventArgs e)
        {
            //this.Style = UiStyle;
            //this.uiLabel1.Style = UiStyle;
            //this.uiLabel2.Style = UiStyle;
            //this.uiTextBoxSmoke.Style = UiStyle;
            //this.uiTextBoxFire.Style = UiStyle;
            //this.uiTrackBarSmoke.Style = UiStyle;
            //this.uiTrackBarFire.Style = UiStyle;
            //this.uiSymbolButtonOk.Style = UiStyle;
            //this.uiSymbolButtonCancel.Style = UiStyle;
            //this.uiLine1.Style = UiStyle;
            this.uiTrackBarSmoke.Value = AlertSmoke;
            this.uiTextBoxSmoke.IntValue = AlertSmoke;
            this.uiTrackBarFire.Value = AlertFire;
            this.uiTextBoxFire.IntValue = AlertFire;
        }

        private void uiTrackBarSmoke_ValueChanged(object sender, EventArgs e)
        {
            this.uiTextBoxSmoke.IntValue = this.uiTrackBarSmoke.Value;
        }

        private void uiTextBoxSmoke_TextChanged(object sender, EventArgs e)
        {
            this.uiTrackBarSmoke.Value = this.uiTextBoxSmoke.IntValue;
        }

        private void uiTrackBarFire_ValueChanged(object sender, EventArgs e)
        {
            this.uiTextBoxFire.IntValue = this.uiTrackBarFire.Value;
        }

        private void uiTextBoxFire_TextChanged(object sender, EventArgs e)
        {
            this.uiTrackBarFire.Value = this.uiTextBoxFire.IntValue;
        }

        private void uiSymbolButtonOk_Click(object sender, EventArgs e)
        {
            AlertSmoke = this.uiTextBoxSmoke.IntValue;
            AlertFire = this.uiTextBoxFire.IntValue;
            this.Close();
        }

        private void uiSymbolButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
