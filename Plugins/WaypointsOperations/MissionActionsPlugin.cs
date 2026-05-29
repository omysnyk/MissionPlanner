using MissionPlanner;
using MissionPlanner.Controls;
using MissionPlanner.Plugin;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Crown;
using ReaLTaiizor.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;
using WaypointsOperations;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using Label = System.Windows.Forms.Label;
using Point = System.Drawing.Point;
using ToolTip = System.Windows.Forms.ToolTip;

namespace MissionActionsPlugin
{
    public class MissionActionsPlugin : Plugin
    {
        private readonly ToolTip _toolTip = new ToolTip();


        public Button BtnValidateAltitudes;
        public Button BtnAddApproachWaypoints;
        public Button BtnCopyWaypoints;
        public Button BtnInsertWaypoints;
        public Button BtnReplaceWaypoints;
        public Label LblCurrBuffer;
        public FlowLayoutPanel PnlWaypointsOperations;

        private ApproachBuilder _approachBuilder;
        private WaypointOperations _waypointOperations;
        private ElevationProfileValidator _elevationProfileValidator;

        public override string Name => "Mission Actions";
        
        public override string Version => "1.2.4";

        public override string Author => "Marquise de Carabas";

        public override bool Exit() {
            return true;
        }

        public override bool Init() {
            _approachBuilder = new ApproachBuilder(this);
            _waypointOperations = new WaypointOperations(this);
            _elevationProfileValidator = new ElevationProfileValidator(this);
            CrownHelper.ThemeProvider.Theme.Colors.LightBackground = Color.FromArgb(0x94, 0xc1, 0x1f);
            
            Console.WriteLine($@"[{Name}] Plugin INITIALIZED");
            return true;
        }

        public override bool Loaded()
        {
            var pluginPanel = MainV2.instance.FlightPlanner.panelWaypoints;
            //var pluginPanel = MainV2.instance.FlightPlanner.flowLayoutPanel1;
            LblCurrBuffer = new Label {
                Text = @"Buffer: Empty",
                Width = 200
            };

            BtnValidateAltitudes = CreateIconButtonAt("Validate mission altitudes", Properties.Resources.altitude_icon_24, 
                (sender, e) => _elevationProfileValidator.ValidateElevationProfile());
            BtnAddApproachWaypoints = CreateIconButtonAt("Add mission approach waypoints", Properties.Resources.approach4_24pi, 
                    (sender, e) => _approachBuilder.AddApproachPoints());
            BtnAddApproachWaypoints.Margin = new Padding(2, 2, 6, 2);

            BtnCopyWaypoints = CreateIconButtonAt("Copy waypoints to buffer", Properties.Resources.waypoints_copy_24pi, (sender, args) => _waypointOperations.CopyWaypointsToBuffer());
            BtnInsertWaypoints = CreateIconButtonAt("Insert waypoints from buffer after selected point", Properties.Resources.waypoints_insert_24pi, (sender, args) => _waypointOperations.InsertBufferWaypoints());
            BtnReplaceWaypoints = CreateIconButtonAt("Replace selected waypoints with copied to buffer", Properties.Resources.replacle_24pi, (sender, args) => _waypointOperations.ReplaceSelectedWithBufferWaypoints());

            PnlWaypointsOperations = new FlowLayoutPanel();
            PnlWaypointsOperations.FlowDirection = FlowDirection.LeftToRight;
            PnlWaypointsOperations.WrapContents = true;
            PnlWaypointsOperations.AutoScroll = true;
            PnlWaypointsOperations.Name = "MissionActionsPlugin";

            PnlWaypointsOperations.Dock = DockStyle.Right;
            PnlWaypointsOperations.Width = 490;
            pluginPanel.SuspendLayout();
            PnlWaypointsOperations.SuspendLayout();

            PnlWaypointsOperations.Controls.Add(BtnValidateAltitudes);
            PnlWaypointsOperations.Controls.Add(BtnAddApproachWaypoints);
            PnlWaypointsOperations.Controls.Add(BtnCopyWaypoints);
            PnlWaypointsOperations.Controls.Add(BtnInsertWaypoints);
            PnlWaypointsOperations.Controls.Add(BtnReplaceWaypoints);
            PnlWaypointsOperations.Controls.Add(LblCurrBuffer);

            pluginPanel.Controls.Add(PnlWaypointsOperations);

            PnlWaypointsOperations.ResumeLayout(false);
            pluginPanel.ResumeLayout(false);
            Console.WriteLine($@"[{Name}] Plugin LOADED");

            return true;
        }

        private MyButton CreateButtonAt(string buttonText, string tooltip, int col, int row, EventHandler clickHandler) {
            const int ButtonWidth = 75;
            const int ButtonHeight = 23;

            var button = new MyButton {
                Text = buttonText,
                Size = new Size(ButtonWidth, ButtonHeight)
            };
            button.Click += clickHandler;

            _toolTip.SetToolTip(button, tooltip);

            return button;
        }

        private Button CreateIconButtonAt(string tooltip, Bitmap icon, EventHandler clickHandler) {
            var size = new Size(32, 32);
            var button = new CrownButton {
                ButtonStyle = ButtonStyle.Normal,
                Text = "",
                Image = icon,
                TextImageRelation = TextImageRelation.Overlay,
                Size = size,
                Padding = new Padding{All = 0},
                Margin = new Padding(2)
            };


            button.Click += clickHandler;

            _toolTip.SetToolTip(button, tooltip);

            return button;
        }

        private Image ResizeImage(Image source, Size targetSize) {
            var bmp = new Bitmap(targetSize.Width, targetSize.Height);
            var g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(source, 0, 0, targetSize.Width, targetSize.Height);
            return bmp;
        }

        private ComboBox CreateComboAt(string[] items, int col, int row) {
            const int boxWdth = 35;
            const int boxHorisontalMargins = 6;
            const int boxHeight = 23;
            const int boxVerticalMargins = 4;

            var combobox = new ComboBox {
                Location = new Point(4 + (boxWdth + boxHorisontalMargins) * col,
                    4 + (boxHeight + boxVerticalMargins) * row),
                Size = new Size(boxWdth, boxHeight)
            };
            combobox.Items.AddRange(items);

            return combobox;
        }
    }
}
