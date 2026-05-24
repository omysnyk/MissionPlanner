using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public readonly List<RuleAssignment> RulesAssignments;
        private readonly List<int> _waypoints;
        private readonly List<(int, int)?> _routeWaypointSegments;
        private readonly List<(Color, Color)> _palette;
        private int _colorCount;

        private (Color, Color) _currenRuleColor;
        private int _selectedSegmentStartWp = -1;
        private int _selectedSegmentEndWp = -1;
        private int _selectedRule = -1;

        private const int GridColumnsCount = 26;
        private const int GridRowsCount = 5;

        public AltitudeValidationParamForm(List<int> waypoints, List<ValidationRule> validationRules, List<RuleAssignment> rulesAssignments)
        {
            Rules = validationRules;
            RulesAssignments = rulesAssignments;

            _waypoints = waypoints;
            _routeWaypointSegments = new List<(int, int)?>();
            for (var i = 1; i < _waypoints.Count; i++)
            {
                _routeWaypointSegments.Add((_waypoints[i - 1], _waypoints[i]));
            }
            _palette = GetRandomPalette(_routeWaypointSegments.Count / 2);
            _currenRuleColor = _palette[_colorCount++ % _palette.Count];

            InitializeComponent();

            FillRulesTable();
            FillRulesAssignmentTable();
            
            validateButton.Enabled = rulesAssignments.Any();

            const int defaultTargetAlt = 70;
            const int defaultMinAlt = 50;
            const int defaultMaxAlt = 100;
            targetAltTextBox.Text = $@"{defaultTargetAlt}";
            minAltTextBox.Text = $@"{defaultMinAlt}";
            maxAltTextBox.Text = $@"{defaultMaxAlt}";

            InitTheme();
        }

        private void InitTheme()
        {
            var materialManager = MaterialManager.Instance;

            materialManager.AddFormToManage(this);
            materialManager.Theme = MaterialManager.Themes.DARK;
            materialManager.ColorScheme = new MaterialColorScheme(MaterialPrimary.LightGreen900,
                MaterialPrimary.LightGreen700, MaterialPrimary.LightGreen100, MaterialAccent.Yellow400,
                MaterialTextShade.WHITE);
        }

        private List<(Color, Color)> GetRandomPalette(int colorCount)
        {
            var p = ColorUtils.GeneratePairPalette(colorCount);
            p.Shuffle();
            return p;
        }

        private void addRuleButton_Click(object sender, EventArgs e)
        {
            var newItem = new ListViewItem(new[]
            {
                $"{Rules.LastOrDefault().RuleId + 1}",
                maxAltTextBox.Text,
                targetAltTextBox.Text,
                minAltTextBox.Text,
                altModeSwitch.Checked ? nameof(AltitudeValidationMode.ASL)  : nameof(AltitudeValidationMode.AGL)
            });
            newItem.SubItems.Add("").Tag = _currenRuleColor.Item1;

            rulesListView.Items.Add(newItem);
            Rules.Add(
                new ValidationRule
                {
                    RuleId = Rules.LastOrDefault().RuleId + 1,
                    MaxAlt = int.Parse(maxAltTextBox.Text),
                    MinAlt = int.Parse(minAltTextBox.Text),
                    TargetAlt = int.Parse(targetAltTextBox.Text),
                    //SegmentEnd = _waypoints[_selectedRangeEnd],
                    ValidationMode = altModeSwitch.Checked ? AltitudeValidationMode.ASL : AltitudeValidationMode.AGL,
                    RuleColor = _currenRuleColor.Item1
                });
            /*addRuleButton.Enabled = false;
            validateButton.Enabled = _selectedRangeEnd == _waypoints.Count - 1;

            startWaypointTextBox.Text = $"{_waypoints[_selectedRangeEnd]}";
            _selectedRangeStart = _selectedRangeEnd + 1;
            _selectedRangeEnd = -1;

            if (_selectedRangeStart >= _waypoints.Count)
            {
                return;
            }

            
            endWaypointTextBox.Text = "";*/
            _currenRuleColor = _palette[_colorCount++ % _palette.Count];
            /*var button = GetButtonByIndex(_selectedRangeStart);
            button.PrimaryColor = _currenRangeColor.Item1;
            button.Invalidate();*/
            rulesListView.Invalidate();
        }

        private void FillRulesTable()
        {
            rulesListView.SuspendLayout();
            for (var index = 0; index < Rules.Count; index++)
            {
                var rule = Rules[index];
                // _selectedRangeEnd = rule.SegmentEnd;
                var newItem = new ListViewItem(new[]
                {
                    // $@"{startWaypoint}",
                    $@"{index + 1}",
                    $"{rule.MaxAlt}",
                    $"{rule.TargetAlt}",
                    $"{rule.MinAlt}",
                    $"{rule.ValidationMode}"
                });
                newItem.SubItems.Add("").Tag = rule.RuleColor;
                rulesListView.Items.Add(newItem);
            }
        }

        private void FillRulesAssignmentTable()
        {
            rulesAssignmentsListView.SuspendLayout();
            for (var index = 0; index < RulesAssignments.Count; index++)
            {
                var ruleAssigment = RulesAssignments[index];
                var newItem = new ListViewItem(new[]
                {
                    $@"{ruleAssigment.SegmentStart}",
                    $"{ruleAssigment.SegmentEnd}",
                    $"{ruleAssigment.Rule.RuleId}"
                });
                newItem.SubItems.Add("").Tag = ruleAssigment.Rule.RuleColor;
                rulesAssignmentsListView.Items.Add(newItem);
            }
            rulesAssignmentsListView.ResumeLayout(true);
        }

        private void FillStartWaypoints()
        {
            var waypoints = _routeWaypointSegments.Where(segment => segment != null).Select(tuple => $"{tuple.Value.Item1}").ToArray<object>();
            
            segmentStartWPComboBox.SuspendLayout();
            segmentStartWPComboBox.Items.Clear();
            segmentStartWPComboBox.Items.AddRange(waypoints);
            if (waypoints.Any())
                segmentStartWPComboBox.SelectedIndex = 0;
            segmentStartWPComboBox.ResumeLayout(true);
        }

        private void FillEndWaypoints()
        {
            if (_selectedSegmentStartWp == -1)
            {
                return;
            }
            segmentEndWPComboBox.SuspendLayout();
            segmentEndWPComboBox.Items.Clear();
            segmentEndWPComboBox.Enabled = false;
            var selectedSegmentIndex = _routeWaypointSegments.FindIndex(tuple => tuple?.Item1 == _selectedSegmentStartWp);
            if (selectedSegmentIndex == -1)
            {
                return;
            }

            var endSegmentsAvailable = new List<object> { $"{_routeWaypointSegments[selectedSegmentIndex]?.Item2}" };
            for (var i = selectedSegmentIndex + 1; i < _routeWaypointSegments.Count; i++)
            {
                if (_routeWaypointSegments[i] == null)
                {
                    break;
                }
                endSegmentsAvailable.Add($"{_routeWaypointSegments[i]?.Item2}");
            }
            segmentEndWPComboBox.Items.AddRange(endSegmentsAvailable.ToArray());
            segmentEndWPComboBox.SelectedIndex = 0;
            segmentEndWPComboBox.Enabled = true;
            segmentEndWPComboBox.ResumeLayout(true);
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void clearRulesButton_Click(object sender, EventArgs e)
        {
            _selectedRule = -1;
            Rules.Clear();
            RulesAssignments.Clear();
            rulesListView.Items.Clear();
            rulesAssignmentsListView.Items.Clear();
            rulesListView.Invalidate();
            rulesAssignmentsListView.Invalidate();
            validateButton.Enabled = false;
            segmentStartWPComboBox.Enabled = false;
            segmentEndWPComboBox.Enabled = false;
        }

        private void rulesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = rulesListView.SelectedIndices;
            if (selection.Count == 0)
            {
                segmentStartWPComboBox.Enabled = false;
                segmentEndWPComboBox.Enabled = false;
                segmentEndWPComboBox.Items.Clear();
                assignRuleButton.Enabled = false;
                return;
            }

            if (selection.Count > 1)
            {
                return;
            }

            if (_selectedRule == -1)
            {
                FillStartWaypoints();
            }

            _selectedRule = selection[0];
            segmentStartWPComboBox.Enabled = true;
            
            var validationRule = Rules[_selectedRule];
            maxAltTextBox.Text = $@"{validationRule.MaxAlt}";
            minAltTextBox.Text = $@"{validationRule.MinAlt}";
            targetAltTextBox.Text = $@"{validationRule.TargetAlt}";
            
            altModeSwitch.CheckState = validationRule.ValidationMode == AltitudeValidationMode.AGL 
                ? CheckState.Unchecked 
                : CheckState.Checked;
        }

        private void segmentStartWPComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedSegmentStartWp = Convert.ToInt32(segmentStartWPComboBox.SelectedItem);
            FillEndWaypoints();
        }

        private void segmentEndWPComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedSegmentEndWp = Convert.ToInt32(segmentEndWPComboBox.SelectedItem);
            assignRuleButton.Enabled = true;
        }

        private void assignRuleButton_Click(object sender, EventArgs e)
        {
            var ruleAssingment = new RuleAssignment
            {
                SegmentStart =  _selectedSegmentStartWp,
                SegmentEnd =  _selectedSegmentEndWp,
                Rule = Rules[_selectedRule]
            };
            
            var newItem = new ListViewItem(new[]
            {
                $"{ruleAssingment.SegmentStart}",
                $"{ruleAssingment.SegmentEnd}",
                $"{ruleAssingment.Rule.RuleId}:[{ruleAssingment.Rule.MaxAlt}<{ruleAssingment.Rule.TargetAlt}>{ruleAssingment.Rule.MinAlt}] {ruleAssingment.Rule.ValidationMode}"
            });
            newItem.SubItems.Add("").Tag = ruleAssingment.Rule.RuleColor;
            rulesAssignmentsListView.Items.Add(newItem);
            rulesAssignmentsListView.Invalidate();
            
            for (var i = _routeWaypointSegments.Count - 1; i >= 0; i--)
            {
                if (_routeWaypointSegments[i]?.Item2 <= ruleAssingment.SegmentEnd && _routeWaypointSegments[i]?.Item1 >= ruleAssingment.SegmentStart)
                {
                    _routeWaypointSegments[i] = null;
                }
            }
            
            FillStartWaypoints();
            FillEndWaypoints();
            
            RulesAssignments.Add(ruleAssingment);
            validateButton.Enabled = true;
        }

        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null)
                return;
            
            var bounds = e.Bounds;
            bool isLastColumn = e.ColumnIndex == listView.Columns.Count - 1;

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

        private void altModeSelector_CheckedChanged(object sender, EventArgs e)
        {
            switch (altModeSwitch.CheckState)
            {
                case CheckState.Checked:
                    altModeSwitch.Text = nameof(AltitudeValidationMode.ASL);
                    break;
                case CheckState.Unchecked:
                    altModeSwitch.Text = nameof(AltitudeValidationMode.AGL);
                    break;
            }

            if (_selectedRule < 0)
            {
                return;
            }
            
            var validationRule = Rules[_selectedRule];
            validationRule.ValidationMode = altModeSwitch.CheckState == CheckState.Checked
                ? AltitudeValidationMode.ASL 
                : altModeSwitch.CheckState == CheckState.Unchecked
                ? AltitudeValidationMode.AGL 
                : validationRule.ValidationMode;
            
            Rules[_selectedRule] = validationRule;

            rulesListView.Items[_selectedRule].SubItems[4].Text = $@"{validationRule.ValidationMode}";
        }

        private void targetAltTextBox_Validating(object sender, CancelEventArgs e)
        {
            ValidateAlt(e, targetAltTextBox);
        }

        private void ValidateAlt(CancelEventArgs e, MaterialTextBox altTextBox)
        {
            if (string.IsNullOrWhiteSpace(altTextBox.Text))
            {
                errorProvider.SetError(altTextBox, "Field is required");
                e.Cancel = true;  // keeps focus on textBox1
            }
            else if (!int.TryParse(altTextBox.Text, out _))
            {
                errorProvider.SetError(altTextBox, "Must be a number");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(altTextBox, "");  // clear error
            }
        }

        private void targetAltTextBox_Validated(object sender, EventArgs e)
        {
            if (_selectedRule < 0)
            {
                return;
            }
            
            var validationRule = Rules[_selectedRule];
            validationRule.MaxAlt = Convert.ToInt32(maxAltTextBox.Text);
            Rules[_selectedRule] = validationRule;

            rulesListView.Items[_selectedRule].SubItems[1].Text = maxAltTextBox.Text;
        }

        private void maxAltTextBox_Validated(object sender, EventArgs e)
        {
            if (_selectedRule < 0)
            {
                return;
            }
            
            var validationRule = Rules[_selectedRule];
            validationRule.TargetAlt = Convert.ToInt32(targetAltTextBox.Text);
            Rules[_selectedRule] = validationRule;

            rulesListView.Items[_selectedRule].SubItems[2].Text = targetAltTextBox.Text;
        }

        private void maxAltTextBox_Validating(object sender, CancelEventArgs e)
        {
            ValidateAlt(e, maxAltTextBox);
        }

        private void minAltTextBox_Validated(object sender, EventArgs e)
        {
            if (_selectedRule < 0)
            {
                return;
            }
            
            var validationRule = Rules[_selectedRule];
            validationRule.MinAlt = Convert.ToInt32(minAltTextBox.Text);
            Rules[_selectedRule] = validationRule;

            rulesListView.Items[_selectedRule].SubItems[3].Text = minAltTextBox.Text;
        }

        private void minAltTextBox_Validating(object sender, CancelEventArgs e)
        {
            ValidateAlt(e, minAltTextBox);
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

        private void clearRuleAssignmentsButton_Click(object sender, EventArgs e)
        {
            _routeWaypointSegments.Clear();
            for (var i = 1; i < _waypoints.Count; i++)
            {
                _routeWaypointSegments.Add((_waypoints[i - 1], _waypoints[i]));
            }
            
            FillStartWaypoints();
            FillEndWaypoints();
            RulesAssignments.Clear();
            rulesAssignmentsListView.Items.Clear();
            rulesAssignmentsListView.Invalidate();
            validateButton.Enabled = false;
        }
    }

    public struct RuleAssignment
    {
        public ValidationRule Rule { get; set; }
        public int SegmentStart { get; set; }
        public int SegmentEnd { get; set; }
    }

    public struct ValidationRule
    {
        public int RuleId { get; set; }
        public int MaxAlt { get; set; }
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
            
            if (elevation >= MaxAlt)
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