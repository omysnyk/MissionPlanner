using System;
using System.Collections.Generic;
using System.Globalization;
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

        private readonly List<int> _waypoints;
        
        public ApproachParametersForm(List<int> waypoints)
        {
            _waypoints = waypoints;
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
            TrackerApproachActivationDistance = Convert.ToSingle(double.Parse( trackerApproachDistTextBox.Text.Trim().Replace(',', '.'), CultureInfo.InvariantCulture));
            ChangeSpeedActivationDistance = Convert.ToSingle(double.Parse( changeSpeedDistTextBox1.Text.Trim().Replace(',', '.'), CultureInfo.InvariantCulture));
            ChangeSpeedBeforeApproach = stabilizeSpeedSwitch.CheckState == CheckState.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void stabilizeSpeedSwitch_CheckedChanged(object sender, EventArgs e)
        {
            changeSpeedDistTextBox1.Enabled = stabilizeSpeedSwitch.Checked;
        }
    }
}