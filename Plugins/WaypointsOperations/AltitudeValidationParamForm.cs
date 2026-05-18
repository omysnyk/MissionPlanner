using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Accord.Math;
using ReaLTaiizor.Colors;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Util;

namespace MissionActionsPlugin
{
    public partial class AltitudeValidationParamForm : MaterialForm
    {
        public readonly List<ValidationRule> Rules;
        private readonly List<int> _waypoints;

        public AltitudeValidationParamForm(List<int> waypoints, List<ValidationRule> validationRules)
        {
            Rules = validationRules;
            const int defaultTargetAlt = 70;
            const int defaultMinAlt = 50;
            _waypoints = waypoints;
            var p = ColorUtils.GeneratePairPalette(_waypoints.Count / 2);
            p.Shuffle();
            _palette = p;
            _currenRangeColor = _palette[_colorCount++ % _palette.Count];

            InitializeComponent();

            FillRulesTable();
            FillRouteGrid();

            targetAltTextBox.Text = $@"{defaultTargetAlt}";
            minAltTextBox.Text = $@"{defaultMinAlt}";

            var materialManager = MaterialManager.Instance;

            materialManager.AddFormToManage(this);
            materialManager.Theme = MaterialManager.Themes.DARK;
            materialManager.ColorScheme = new MaterialColorScheme(MaterialPrimary.LightGreen900,
                MaterialPrimary.LightGreen700, MaterialPrimary.LightGreen100, MaterialAccent.Yellow400,
                MaterialTextShade.WHITE);
        }

        private void altModeSelector_CheckedChanged(object sender, EventArgs e)
        {
            switch (altModeSwitch.CheckState)
            {
                case CheckState.Checked:
                    altModeSwitch.Text = "ASL";
                    break;
                case CheckState.Unchecked:
                    altModeSwitch.Text = "AGL";
                    break;
            }
        }

        private void addRuleButton_Click(object sender, EventArgs e)
        {
            var newItem = new ListViewItem(new[]
            {
                $@"{startWaypointTextBox.Text}",
                $@"{endWaypointTextBox.Text}",
                targetAltTextBox.Text,
                minAltTextBox.Text,
                altModeSwitch.Checked ? "ASL" : "AGL"
            });
            newItem.SubItems.Add("").Tag = _currenRangeColor.Item1;

            rulesListView.Items.Add(newItem);
            Rules.Add(
                new ValidationRule
                {
                    MinAlt = int.Parse(minAltTextBox.Text),
                    TargetAlt = int.Parse(targetAltTextBox.Text),
                    SegmentEnd = _waypoints[_selectedRangeEnd],
                    ValidationMode = altModeSwitch.Checked ? AltitudeValidationMode.ASL : AltitudeValidationMode.AGL,
                    RuleColor = _currenRangeColor.Item1
                });
            addRuleButton.Enabled = false;
            okButton.Enabled = _selectedRangeEnd == _waypoints.Count - 1;

            startWaypointTextBox.Text = $"{_waypoints[_selectedRangeEnd]}";
            _selectedRangeStart = _selectedRangeEnd + 1;
            _selectedRangeEnd = -1;

            if (_selectedRangeStart >= _waypoints.Count)
            {
                return;
            }

            
            endWaypointTextBox.Text = "";
            _currenRangeColor = _palette[_colorCount++ % _palette.Count];
            var button = GetButtonByIndex(_selectedRangeStart);
            button.PrimaryColor = _currenRangeColor.Item1;
            button.Invalidate();
            rulesListView.Invalidate();
        }

        private int _colorCount = 0;
        private readonly Color _defaultWaypointColor = Color.Silver;

        private readonly List<(Color, Color)> _palette;
        private (Color, Color) _currenRangeColor;
        private int _selectedRangeStart = -1;
        private int _selectedRangeEnd = -1;

        private const int GridColumnsCount = 26;
        private const int GridRowsCount = 5;

        private void FillRulesTable()
        {
            _selectedRangeStart = 0;
            var startWaypoint = _waypoints[0];
            startWaypointTextBox.Text = $"{startWaypoint}";
            foreach (var rule in Rules)
            {
                _selectedRangeEnd = rule.SegmentEnd;
                var newItem = new ListViewItem(new[]
                {
                    $@"{startWaypoint}",
                    $@"{rule.SegmentEnd}",
                    $"{rule.TargetAlt}",
                    $"{rule.MinAlt}",
                    $"{rule.ValidationMode}"
                });
                newItem.SubItems.Add("").Tag = rule.RuleColor;
                rulesListView.Items.Add(newItem);
                
                startWaypointTextBox.Text = $"{startWaypoint}";
                startWaypoint = rule.SegmentEnd;
                endWaypointTextBox.Text = $"{rule.SegmentEnd}";
            }

            var rulesMatchesLastWaypoint = startWaypoint == _waypoints[_waypoints.Count - 1];
            _selectedRangeEnd = rulesMatchesLastWaypoint ? _waypoints.Count - 1 : -1;
            _selectedRangeStart = _selectedRangeEnd + 1;
            okButton.Enabled = rulesMatchesLastWaypoint;
            addRuleButton.Enabled = false;
        }

        private void FillRouteGrid()
        {
            var rulesMap = new SortedList<int, ValidationRule>();
            foreach (var rule in Rules)
            {
                rulesMap[rule.SegmentEnd] = rule;
            }

            for (var i = 0; i < _waypoints.Count; i++)
            {
                var row = i / GridColumnsCount;
                var column = i % GridColumnsCount;

                CeilKey(rulesMap, _waypoints[i], out var rule);
                var waypointButton = new HopeButton
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                    AutoSize = false,
                    PrimaryColor = rule.RuleColor.IsEmpty ? _defaultWaypointColor : rule.RuleColor,
                    Margin = new Padding(1),
                    Name = $@"{_waypoints[i]}",
                    Text = $@"{_waypoints[i]}",
                    Size = new Size(32, 32),
                    Tag = i
                };

                var index = i;
                waypointButton.MouseClick += (sender, args) => { RangeButton_Click(index, _waypoints[index]); };
                waypointsTablePanel.Controls.Add(waypointButton, column, row);
            }
            
            CeilKey(rulesMap, _waypoints[0], out var firstWpRule);
            GetButtonByIndex(0).PrimaryColor = firstWpRule.RuleColor.IsEmpty ? _currenRangeColor.Item1 : firstWpRule.RuleColor;
        }

        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var bounds = e.Bounds;
            bool isLastColumn = e.ColumnIndex == rulesListView.Columns.Count - 1;

            if (isLastColumn && e.SubItem.Tag is Color color)
            {
                // Just fill with color, no text
                var brush = new SolidBrush(color);
                e.Graphics.FillRectangle(brush, bounds);
            }
            else
            {
                // Normal text rendering for all other columns
                var bgColor = e.Item.Selected ? SystemColors.Highlight : e.Item.BackColor;
                var fgColor = e.Item.Selected ? SystemColors.HighlightText : e.Item.ForeColor;

                var brush = new SolidBrush(bgColor);
                e.Graphics.FillRectangle(brush, bounds);

                TextRenderer.DrawText(
                    e.Graphics,
                    e.SubItem.Text,
                    e.Item.Font,
                    bounds,
                    fgColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.LeftAndRightPadding
                );
            }
        }

        private void RangeButton_Click(int index, int waypoint)
        {
            if (_selectedRangeStart == index)
            {
                endWaypointTextBox.Text = "";
                var rangeStartWpButton = GetButtonByIndex(index);
                rangeStartWpButton.PrimaryColor = _currenRangeColor.Item1;
                // deselect all
                for (var i = _selectedRangeStart + 1; i <= _selectedRangeEnd; i++)
                {
                    var button = GetButtonByIndex(i);
                    button.PrimaryColor = _defaultWaypointColor;
                    button.Invalidate();
                }

                addRuleButton.Enabled = false;
                _selectedRangeEnd = -1;
            }
            else if (_selectedRangeStart < index)
            {
                if (_selectedRangeEnd > index)
                {
                    for (var i = index; i <= _selectedRangeEnd; i++)
                    {
                        var button = GetButtonByIndex(i);
                        button.PrimaryColor = _defaultWaypointColor;
                        button.Invalidate();
                    }
                }

                endWaypointTextBox.Text = waypoint.ToString();
                _selectedRangeEnd = index;
                for (var i = index; i > _selectedRangeStart; i--)
                {
                    var button = GetButtonByIndex(i);
                    button.PrimaryColor = _currenRangeColor.Item1;
                    button.Invalidate();
                }

                // enable adding rule only if not selected next to last waypoint.
                addRuleButton.Enabled = _selectedRangeEnd != _waypoints.Count - 2;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void clearRulesButton_Click(object sender, EventArgs e)
        {
            _selectedRangeStart = 0;
            _selectedRangeEnd = -1;

            okButton.Enabled = false;
            startWaypointTextBox.Text = $"{_waypoints[0]}";
            // deselect all
            for (var i = 0; i < _waypoints.Count; i++)
            {
                var button = GetButtonByIndex(i);
                button.PrimaryColor = _defaultWaypointColor;
                button.Invalidate();
            }
            
            rulesListView.Items.Clear();
            Rules.Clear();
            
            var startWaypoint = _waypoints[0];
            startWaypointTextBox.Text = $"{startWaypoint}";
            endWaypointTextBox.Text = $"";
            GetButtonByIndex(0).PrimaryColor = _currenRangeColor.Item1;
        }

        private HopeButton GetButtonByIndex(int i)
        {
            var row = i / GridColumnsCount;
            var column = i % GridColumnsCount;

            var button = ((HopeButton)waypointsTablePanel.GetControlFromPosition(column, row));
            return button;
        }

        public static void CeilKey<T>(SortedList<int, T> map, int key, out T value)
        {
            int lo = 0;
            int hi = map.Count - 1;

            while (lo <= hi)
            {
                int mid = lo + ((hi - lo) / 2);

                int midKey = map.Keys[mid];

                if (midKey == key)
                {
                    value = map.Values[mid];
                    return;
                }

                if (midKey < key)
                    lo = mid + 1;
                else
                    hi = mid - 1;
            }

            // lo now points to ceil element
            if (lo < map.Count)
            {
                value = map.Values[lo];
                return;
            }

            value = default;
        }
        
        public static void FloorKey<T>(SortedList<int, T> map, int key, out T value)
        {
            var lo = 0;
            var hi = map.Count - 1;

            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) / 2);

                var midKey = map.Keys[mid];

                if (midKey == key)
                {
                    value = map.Values[mid];
                    return;
                }

                if (midKey < key)
                    lo = mid + 1;
                else
                    hi = mid - 1;
            }

            // hi now points to floor element
            if (hi >= 0)
            {
                value = map.Values[hi];
                return;
            }

            value = default;
        }
    }

    public struct ValidationRule
    {
        public int SegmentEnd { get; set; }
        public int TargetAlt { get; set; }
        public int MinAlt { get; set; }
        public AltitudeValidationMode ValidationMode { get; set; }

        public Color RuleColor { get; set; }

        public double OverlimitDegree(double routeAltitude, double terrainAltitude)
        {
            if (ValidationMode == AltitudeValidationMode.ASL)
            {
                if (routeAltitude < MinAlt)
                {
                    return -1.0;
                }
            
                if (routeAltitude > TargetAlt + 20)
                {
                    return 1.0;
                }
            
                if (routeAltitude < TargetAlt * 0.95)
                {
                    return -0.5;
                }
                
                return 0.0;
            }

            var elevation = routeAltitude - terrainAltitude;

            if (elevation < MinAlt)
            {
                return -1.0;
            }
            
            if (elevation > TargetAlt + 100)
            {
                return 1.0;
            }
            
            if (elevation < TargetAlt * 0.95)
            {
                return -0.5;
            }

            return 0.0;
        }
    }

    public enum AltitudeValidationMode
    {
        AGL,
        ASL
    }
}