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

        private void okButton_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}