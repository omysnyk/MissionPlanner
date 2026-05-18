using System;
using System.Windows.Forms;
using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Util;

namespace MissionActionsPlugin
{
    public partial class ApproachParametersForm : MaterialForm
    {
        public int ApproachAltitude { get; set; }
        public bool ChangeSpeedBeforeApproach { get; set; }
        public float ChangeSpeedActivationDistance = 10;
        public float TrackerApproachActivationDistance { get; set; }
        public int TrackerActivationPoint { get; set; }
        
        public ApproachParametersForm()
        {
            InitializeComponent();
            
            var materialManager = MaterialManager.Instance;

            materialManager.AddFormToManage(this);
            materialManager.Theme = MaterialManager.Themes.DARK;
            materialManager.ColorScheme = new MaterialColorScheme(MaterialPrimary.LightGreen900,
                MaterialPrimary.LightGreen700, MaterialPrimary.LightGreen100, MaterialAccent.Yellow400,
                MaterialTextShade.WHITE);
            ApproachAltitude = 60;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ApproachAltitude = Convert.ToInt32(approachAltComboBox.SelectedItem);
            TrackerActivationPoint = Convert.ToInt32(trackerActivationWpComboBox.SelectedItem);
            //TrackerApproachActivationDistance = Convert.ToSingle(trackerApproachDistTextBox.Text);
            ChangeSpeedBeforeApproach = stabilizeSpeedSwitch.CheckState == CheckState.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}