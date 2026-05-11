using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MissionPlanner;
using MissionPlanner.GCSViews;

namespace MissionActionsPlugin
{
    public class WaypointOperations
    {
        private readonly MissionActionsPlugin _plugin;
        private readonly FlightPlanner _plannerModule;
        private List<List<object>> _copiedCellValues;

        public WaypointOperations(MissionActionsPlugin plugin)
        {
            _plugin = plugin;
            _plannerModule = _plugin.Host.MainForm.FlightPlanner;
        }

        public void ReplaceSelectedWithBufferWaypoints()
        {
            var selectedRows = _plannerModule.Commands.SelectedRows;
            var selectedCells = _plannerModule.Commands.SelectedCells;
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
                _plannerModule.updateUndoBuffer(true);
                _plannerModule.quickadd = true;
                // mono fix
                _plannerModule.Commands.CurrentCell = null;
                _plannerModule.Commands.Rows.RemoveAt(index);
                _plannerModule.quickadd = false;
                _plannerModule.writeKML();
            }

            foreach (var sourceRow in _copiedCellValues)
            {
                _plannerModule.updateUndoBuffer(true);
                _plannerModule.Commands.Rows.Insert(index);
                var newRow = _plannerModule.Commands.Rows[index];
                for (var i = 0; i < newRow.Cells.Count; i++) newRow.Cells[i].Value = sourceRow[i];

                //_plannerModule.Commands.Rows.Insert(index++, rowToInsert);
                Console.WriteLine($@"Inserted row at index {index}");
                _plannerModule.writeKML();
                index++;
            }
        }

        public void InsertBufferWaypoints()
        {
            var selectedRows = _plannerModule.Commands.SelectedRows;
            var selectedCells = _plannerModule.Commands.SelectedCells;

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
                _plannerModule.updateUndoBuffer(true);
                _plannerModule.Commands.Rows.Insert(endIndex);
                var newRow = _plannerModule.Commands.Rows[endIndex];
                for (var i = 0; i < newRow.Cells.Count; i++) newRow.Cells[i].Value = sourceRow[i];

                _plannerModule.writeKML();
                endIndex++;
            }
        }

        public void CopyWaypointsToBuffer()
        {
            var selectedRows = _plannerModule.Commands.SelectedRows;
            var selectedCells = _plannerModule.Commands.SelectedCells;
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

            _plugin.PnlWaypointsOperations.SuspendLayout();
            if (_plannerModule.lbl_wpfile.Text.Contains("Loaded") ||
                _plannerModule.lbl_wpfile.Text.Contains("Saved"))
            {
                var fileName = _plannerModule.lbl_wpfile.Text.Split(' ')[1];
                _plugin.LblCurrBuffer.Text = $@"Buffer: {fileName} [{startIndex + 1}:{endIndex + 1}]";
            }
            else
            {
                _plugin.LblCurrBuffer.Text = $@"Buffer: [{startIndex + 1}:{endIndex + 1}]";
            }

            _plugin.PnlWaypointsOperations.ResumeLayout(true);
        }

        private static (double lat, double lon, double alt) ExtractCoords(DataGridViewRow lastCommand)
        {
            var lat = double.Parse(lastCommand.Cells[5].Value.ToString());
            var lon = double.Parse(lastCommand.Cells[6].Value.ToString());
            var alt = double.Parse(lastCommand.Cells[7].Value.ToString()) / CurrentState.multiplieralt;

            return (lat, lon, alt);
        }

        private ushort ExtractCmd(DataGridViewRow row)
        {
            return _plannerModule.getCmdID(row.Cells[0].Value.ToString());
        }

        private List<DataGridViewRow> GetSelectedRows()
        {
            var flightPlanner = _plannerModule;
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
    }
}