using MissionPlanner.Controls;
using MissionPlanner.Plugin;
using System;
using System.Drawing;
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
        public MyButton BtnFilter;
        public FlowLayoutPanel PnlKmzImporter;
        private OverlaySlidePanel PnlSlideFilter;


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
                Size = new Size(buttonWidth, buttonHeight)
            };
            BtnClearOverlay.Click += (sender, e) => _kmzImporter.ClearOverlay();

            BtnFilter = new MyButton
            {
                Text = @"Filter",
                Size = new Size(buttonWidth, buttonHeight)
            };

            PnlKmzImporter = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Name = "KMZ Importer",
                Width = 123,
                Padding = new Padding {All = 3}
            };

            PnlSlideFilter = new OverlaySlidePanel
            {
                PanelWidth = 260
            };

            BtnFilter.Click += (sender, e) => PnlSlideFilter.Toggle();

            pluginPanel.AutoScroll = false;
            pluginPanel.SuspendLayout();
            PnlKmzImporter.Controls.Add(BtnLoadKmz);
            PnlKmzImporter.Controls.Add(BtnClearOverlay);
            PnlKmzImporter.Controls.Add(BtnFilter);
            PnlSlideFilter.AttachTo(Host.MainForm.FlightPlanner.MainMap);
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
