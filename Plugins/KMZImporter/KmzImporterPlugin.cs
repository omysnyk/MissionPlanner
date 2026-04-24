using MissionPlanner.Controls;
using MissionPlanner.Plugin;
using System;
using System.Windows.Forms;

namespace KMZImporter
{
    public class KmzImporterPlugin : Plugin
    {
        public override string Name => "KMZ Import Utils";

        public override string Version => "0.0.1";

        public override string Author => "Marquise de Carabas";

        public MyButton BtnLoadKmz;
        public MyButton BtnClearOverlay;
        public FlowLayoutPanel PnlKmzImporter;

        public override bool Exit() {
            return true;
        }

        public override bool Init() {
            _kmzImporter = new KmzImporter(this);

            Console.WriteLine($@"[{Name}] Plugin INITIALIZED");
            return true;
        }

        public override bool Loaded()
        {
            var pluginPanel = Host.MainForm.FlightPlanner.flowLayoutPanel1;
            
            var buttonWidth = Host.MainForm.FlightPlanner.BUT_loadwpfile.ClientSize.Width;
            var buttonHeight = Host.MainForm.FlightPlanner.BUT_loadwpfile.Size.Height;
            BtnLoadKmz = new MyButton {
                Text = @"Load KMZ Overlay",
                Size = new System.Drawing.Size(buttonWidth, buttonHeight)
            };

            BtnLoadKmz.Click += (sender, e) => _kmzImporter.ImportKmz();

            BtnClearOverlay = new MyButton {
                Text = @"Clear Overlay",
                Size = new System.Drawing.Size(buttonWidth, buttonHeight)
            };
            BtnClearOverlay.Click += (sender, e) => _kmzImporter.ClearOverlay();

            PnlKmzImporter = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Name = "KMZ Importer",
                Width = 123,
                Padding = new Padding {All = 3}
            };

            // var pluginPanel = Host.MainForm.FlightPlanner.flowLayoutPanel1;
            pluginPanel.SuspendLayout();
            PnlKmzImporter.Controls.Add(BtnLoadKmz);
            PnlKmzImporter.Controls.Add(BtnClearOverlay);
            pluginPanel.Controls.Add(PnlKmzImporter);

            PnlKmzImporter.ResumeLayout(false);
            pluginPanel.ResumeLayout(false);
            // pluginPanel.PerformLayout();
            Console.WriteLine($@"[{Name}] Plugin LOADED");
            return true;
        }



        private KmzImporter _kmzImporter;
    }
}
