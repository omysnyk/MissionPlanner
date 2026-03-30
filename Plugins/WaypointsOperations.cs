using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MissionPlanner.Controls;
using ComboBox = System.Windows.Forms.ComboBox;
using ToolTip = System.Windows.Forms.ToolTip;

namespace MissionPlanner.plugins
{
    public class WaypointsOperations : Plugin.Plugin
    {
        private readonly ToolTip _toolTip = new ToolTip();

        private List<List<object>> _copiedCellValues;
        public MyButton BtnAddApproachWaypoints;
        public MyButton BtnCopyWaypoints;
        public MyButton BtnInsertWaypoints;
        public MyButton BtnReplaceWaypoints;
        public Label LblCurrBuffer;
        public FlowLayoutPanel PnlWaypointsOperations;

        public override string Name => "Waypoints Operations";

        public override string Version => "0.0.2";

        public override string Author => "Marquise de Carabas";

        public override bool Exit()
        {
            return true;
        }

        public override bool Init()
        {
            Console.WriteLine(@"[WAYPOINTS OPERATIONS] Plugin INITIALIZED");
            return true;
        }

        public override bool Loaded()
        {
            var pluginPanel = MainV2.instance.FlightPlanner.panelWaypoints;
            //var pluginPanel = MainV2.instance.FlightPlanner.flowLayoutPanel1;
            LblCurrBuffer = new Label
            {
                Text = @"Buffer: Empty",
                Width = 200
            };
            BtnAddApproachWaypoints =
                CreateButtonAt("Approach", "Add mission approach waypoints", 0, 0, BtnAddApproachClick);
            BtnCopyWaypoints = CreateButtonAt("Copy", "Copy waypoints to buffer", 0, 0, BtnCopyWaypointsClick);
            BtnInsertWaypoints = CreateButtonAt("Insert", "Insert waypoints from buffer after selected point", 0, 0,
                BtnInsertWaypointsClick);
            BtnReplaceWaypoints = CreateButtonAt("Replace", "Replace selected waypoints with copied to buffer", 1, 0,
                BtnReplaceWaypointsClick);

            PnlWaypointsOperations = new FlowLayoutPanel();
            PnlWaypointsOperations.FlowDirection = FlowDirection.LeftToRight;
            PnlWaypointsOperations.WrapContents = true;
            PnlWaypointsOperations.AutoScroll = true;
            PnlWaypointsOperations.Name = "WaypointsOperations";

            PnlWaypointsOperations.Dock = DockStyle.Right;
            PnlWaypointsOperations.Width = 490;
            pluginPanel.SuspendLayout();
            PnlWaypointsOperations.SuspendLayout();

            //PnlWaypointsOperations.Controls.Add(BtnAddApproachWaypoints);
            PnlWaypointsOperations.Controls.Add(BtnCopyWaypoints);
            PnlWaypointsOperations.Controls.Add(BtnInsertWaypoints);
            PnlWaypointsOperations.Controls.Add(BtnReplaceWaypoints);
            PnlWaypointsOperations.Controls.Add(LblCurrBuffer);
            PnlWaypointsOperations.Name = "WaypointsOperations";

            pluginPanel.Controls.Add(PnlWaypointsOperations);

            PnlWaypointsOperations.ResumeLayout(false);
            pluginPanel.ResumeLayout(false);
            Console.WriteLine(@"[WAYPOINTS OPERATIONS] Plugin LOADED");

            return true;
        }

        public void BtnReplaceWaypointsClick(object sender, EventArgs e)
        {
            var selectedRows = MainV2.instance.FlightPlanner.Commands.SelectedRows;
            var selectedCells = MainV2.instance.FlightPlanner.Commands.SelectedCells;
            if (!(_copiedCellValues?.Count > 0))
            {
                CustomMessageBox.Show("Buffer is empty!");
                return;
            }

            if (selectedRows.Count == 0 && selectedCells.Count == 0)
            {
                CustomMessageBox.Show("No target rows selected!");
                return;
            }

            var rowsToReplace = GetSelectedRows();

            var index = rowsToReplace[0].Index;
            var endIndex = rowsToReplace[rowsToReplace.Count - 1].Index;

            if (endIndex != index + rowsToReplace.Count - 1)
            {
                CustomMessageBox.Show("Select sequential rows!");
                return;
            }

            for (var i = 0; i < rowsToReplace.Count; i++)
            {
                MainV2.instance.FlightPlanner.updateUndoBuffer(true);
                MainV2.instance.FlightPlanner.quickadd = true;
                // mono fix
                MainV2.instance.FlightPlanner.Commands.CurrentCell = null;
                MainV2.instance.FlightPlanner.Commands.Rows.RemoveAt(index);
                MainV2.instance.FlightPlanner.quickadd = false;
                MainV2.instance.FlightPlanner.writeKML();
            }

            foreach (var sourceRow in _copiedCellValues)
            {
                MainV2.instance.FlightPlanner.updateUndoBuffer(true);
                MainV2.instance.FlightPlanner.Commands.Rows.Insert(index);
                var newRow = MainV2.instance.FlightPlanner.Commands.Rows[index];
                for (var i = 0; i < newRow.Cells.Count; i++) newRow.Cells[i].Value = sourceRow[i];

                //MainV2.instance.FlightPlanner.Commands.Rows.Insert(index++, rowToInsert);
                Console.WriteLine($@"Inserted row at index {index}");
                MainV2.instance.FlightPlanner.writeKML();
                index++;
            }
        }

        public void BtnInsertWaypointsClick(object sender, EventArgs e)
        {
            var selectedRows = MainV2.instance.FlightPlanner.Commands.SelectedRows;
            var selectedCells = MainV2.instance.FlightPlanner.Commands.SelectedCells;

            if (!(_copiedCellValues?.Count > 0))
            {
                CustomMessageBox.Show("Buffer is empty!");
                return;
            }

            if (selectedRows.Count == 0 && selectedCells.Count == 0)
            {
                CustomMessageBox.Show("No target row selected!");
                return;
            }

            var rowsToReplace = GetSelectedRows();
            var endIndex = rowsToReplace[rowsToReplace.Count - 1].Index + 1;

            foreach (var sourceRow in _copiedCellValues)
            {
                MainV2.instance.FlightPlanner.updateUndoBuffer(true);
                MainV2.instance.FlightPlanner.Commands.Rows.Insert(endIndex);
                var newRow = MainV2.instance.FlightPlanner.Commands.Rows[endIndex];
                for (var i = 0; i < newRow.Cells.Count; i++) newRow.Cells[i].Value = sourceRow[i];

                MainV2.instance.FlightPlanner.writeKML();
                endIndex++;
            }

            // foreach (var rowToInsert in _copiedRowsBuffer) {
            //     MainV2.instance.FlightPlanner.updateUndoBuffer(true);
            //     MainV2.instance.FlightPlanner.Commands.Rows.Insert(++endIndex, rowToInsert);
            //     Console.WriteLine($@"Inserted {_copiedRowsBuffer.IndexOf(rowToInsert)} row at index {endIndex}");
            //     MainV2.instance.FlightPlanner.writeKML();
            // }
        }

        public void BtnCopyWaypointsClick(object sender, EventArgs e)
        {
            var selectedRows = MainV2.instance.FlightPlanner.Commands.SelectedRows;
            var selectedCells = MainV2.instance.FlightPlanner.Commands.SelectedCells;
            if (selectedRows.Count == 0 && selectedCells.Count == 0)
            {
                CustomMessageBox.Show("No rows selected!");
                return;
            }

            var copiedRowsBuffer = GetSelectedRows();

            _copiedCellValues = copiedRowsBuffer.Select((row, index) =>
                (from DataGridViewCell cell in row.Cells select cell.Value).ToList()).ToList();

            var startIndex = copiedRowsBuffer[0].Index;
            var endIndex = copiedRowsBuffer[copiedRowsBuffer.Count - 1].Index;

            PnlWaypointsOperations.SuspendLayout();
            if (MainV2.instance.FlightPlanner.lbl_wpfile.Text.Contains("Loaded") ||
                MainV2.instance.FlightPlanner.lbl_wpfile.Text.Contains("Saved"))
            {
                var fileName = MainV2.instance.FlightPlanner.lbl_wpfile.Text.Split(' ')[1];
                LblCurrBuffer.Text = $@"Buffer: {fileName} [{startIndex + 1}:{endIndex + 1}]";
            }
            else
            {
                LblCurrBuffer.Text = $@"Buffer: [{startIndex + 1}:{endIndex + 1}]";
            }

            PnlWaypointsOperations.ResumeLayout(true);
        }


        public void BtnAddApproachClick(object sender, EventArgs e)
        {
            var approachAlt = 60;
            if (InputBox.Show("Approach Parameters", "Select approach altitude above target", ref approachAlt) !=
                DialogResult.OK)
            {
            }
        }

        private List<DataGridViewRow> GetSelectedRows()
        {
            var flightPlanner = MainV2.instance.FlightPlanner;
            var commands = flightPlanner.Commands;
            var selectedRows = commands.SelectedRows;
            var selectedCells = commands.SelectedCells;
            var targetRows = new List<DataGridViewRow>();
            if (selectedRows.Count > 0)
                targetRows.AddRange(from DataGridViewRow item in selectedRows select item);
            else if (selectedCells.Count > 0)
                targetRows.AddRange(from DataGridViewCell cell in selectedCells select cell.OwningRow);

            targetRows.Sort((a, b) => a.Index.CompareTo(b.Index));

            return targetRows;
        }

        private MyButton CreateButtonAt(string buttonText, string tooltip, int col, int row, EventHandler clickHandler)
        {
            const int ButtonWidth = 75;
            const int ButtonHeight = 23;

            var button = new MyButton
            {
                Text = buttonText,
                Size = new Size(ButtonWidth, ButtonHeight)
            };
            button.Click += clickHandler;

            _toolTip.SetToolTip(button, tooltip);

            return button;
        }

        private ComboBox CreateComboAt(string[] items, int col, int row)
        {
            const int boxWdth = 35;
            const int boxHorisontalMargins = 6;
            const int boxHeight = 23;
            const int boxVerticalMargins = 4;

            var combobox = new ComboBox
            {
                Location = new Point(4 + (boxWdth + boxHorisontalMargins) * col,
                    4 + (boxHeight + boxVerticalMargins) * row),
                Size = new Size(boxWdth, boxHeight)
            };
            combobox.Items.AddRange(items);

            return combobox;
        }
    }
}