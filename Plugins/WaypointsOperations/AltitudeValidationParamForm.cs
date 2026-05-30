using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Accord.Math;
using MissionPlanner.Utilities;
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
        private bool _isEditRule = false;

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
            _palette = ColorUtils.GeneratePairPalette(1 + _routeWaypointSegments.Count / 2);
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

        private void addRuleButton_Click(object sender, EventArgs e)
        {
            var maxAltEnabled = maxAltCheckBox.Checked;
            var maxAlt = maxAltEnabled ? int.Parse(maxAltTextBox.Text) : -1;
            var minAlt = int.Parse(minAltTextBox.Text);
            var targetAlt = int.Parse(targetAltTextBox.Text);

            if (_isEditRule)
            {
                var validationRule = Rules[_selectedRule];
                validationRule.ValidationMode = altModeSwitch.CheckState == CheckState.Checked
                    ? AltitudeValidationMode.ASL 
                    : altModeSwitch.CheckState == CheckState.Unchecked
                        ? AltitudeValidationMode.AGL 
                        : validationRule.ValidationMode;

                validationRule.MaxAlt = maxAlt;
                validationRule.MaxAltEnabled = maxAltEnabled;
                validationRule.MinAlt = minAlt;
                validationRule.TargetAlt = targetAlt;
            
                Rules[_selectedRule] = validationRule;
                rulesListView.Items[_selectedRule] = ItemFromRule(validationRule);
                rulesListView.Invalidate();
                return;
            }

            var newItem = new ListViewItem(new[]
            {
                $"{Rules.LastOrDefault().RuleId + 1}",
                maxAltEnabled ? maxAltTextBox.Text : "-",
                targetAltTextBox.Text,
                minAltTextBox.Text,
                altModeSwitch.Checked ? nameof(AltitudeValidationMode.ASL)  : nameof(AltitudeValidationMode.AGL)
            });
            newItem.SubItems.Add("").Tag = _currenRuleColor.Item1;

            rulesListView.Items.Add(newItem);
            

            if (minAlt > targetAlt)
            {
                MessageBox.Show(@"Min altitude expected to be less than target altitude", @"Invalid altitude",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (maxAlt < targetAlt && maxAltEnabled)
            {
                MessageBox.Show(@"Max altitude expected to be greater than target altitude", @"Invalid altitude",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Rules.Add(
                new ValidationRule
                {
                    RuleId = Rules.LastOrDefault().RuleId + 1,
                    MaxAltEnabled = maxAltEnabled,
                    MaxAlt = maxAlt,
                    MinAlt = minAlt,
                    TargetAlt = targetAlt,
                    ValidationMode = altModeSwitch.Checked ? AltitudeValidationMode.ASL : AltitudeValidationMode.AGL,
                    RuleColor = _currenRuleColor.Item1
                });
            
            _currenRuleColor = _palette[_colorCount++ % _palette.Count];
            rulesListView.Invalidate();
        }

        private static ListViewItem ItemFromRule(ValidationRule validationRule)
        {
            var item = new ListViewItem(new[]
            {
                $"{validationRule.RuleId}",
                validationRule.MaxAltEnabled ? $"{validationRule.MaxAlt}" : "-",
                $"{validationRule.TargetAlt}",
                $"{validationRule.MinAlt}",
                $"{validationRule.ValidationMode}"
            });
            item.SubItems.Add("").Tag = validationRule.RuleColor;
            
            return item;
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
                rulesAssignmentsListView.Items.Add(ItemFromRuleAssignment(ruleAssigment));
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
            addRuleButton.Text = @"Add Rule";
        }

        private void rulesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = rulesListView.SelectedIndices;
            if (selection.Count == 0)
            {
                _isEditRule = false;
                addRuleButton.Text = @"Add Rule";
                segmentStartWPComboBox.Enabled = false;
                segmentEndWPComboBox.Enabled = false;
                segmentEndWPComboBox.Items.Clear();
                assignRuleButton.Enabled = false;
                return;
            }

            if (selection.Count > 1)
            {
                _isEditRule = false;
                return;
            }

            if (_selectedRule == -1)
            {
                FillStartWaypoints();
            }

            _isEditRule = true;
            addRuleButton.Text = @"Save Rule";
            _selectedRule = selection[0];
            segmentStartWPComboBox.Enabled = true;
            
            var validationRule = Rules[_selectedRule];
            maxAltCheckBox.Checked = validationRule.MaxAltEnabled;
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
            if (_selectedRule < 0)
            {
                return;
            }
            
            var ruleAssignment = new RuleAssignment
            {
                SegmentStart =  _selectedSegmentStartWp,
                SegmentEnd =  _selectedSegmentEndWp,
                Rule = Rules[_selectedRule]
            };
            RulesAssignments.Add(ruleAssignment);
            RulesAssignments.Sort((assignment, assignment1) => { var cmp = assignment.SegmentStart.CompareTo(assignment1.SegmentStart);
                return cmp != 0 ? cmp : assignment.SegmentEnd.CompareTo(assignment1.SegmentEnd);});
            
            rulesAssignmentsListView.Items.Clear();
            rulesAssignmentsListView.Items.AddRange(RulesAssignments.Select(ItemFromRuleAssignment).ToArray());
            rulesAssignmentsListView.Invalidate();
            
            for (var i = _routeWaypointSegments.Count - 1; i >= 0; i--)
            {
                if (_routeWaypointSegments[i]?.Item2 <= ruleAssignment.SegmentEnd && _routeWaypointSegments[i]?.Item1 >= ruleAssignment.SegmentStart)
                {
                    _routeWaypointSegments[i] = null;
                }
            }
            
            FillStartWaypoints();
            FillEndWaypoints();
            validateButton.Enabled = true;
        }

        private static ListViewItem ItemFromRuleAssignment(RuleAssignment ruleAssignment)
        {
            var newItem = new ListViewItem(new[]
            {
                $"{ruleAssignment.SegmentStart}",
                $"{ruleAssignment.SegmentEnd}",
                $"{ruleAssignment.Rule.RuleId}:[{ruleAssignment.Rule.MaxAlt}<{ruleAssignment.Rule.TargetAlt}>{ruleAssignment.Rule.MinAlt}] {ruleAssignment.Rule.ValidationMode}"
            });
            newItem.SubItems.Add("").Tag = ruleAssignment.Rule.RuleColor;
            return newItem;
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
            
            // var validationRule = Rules[_selectedRule];
            // var mode = altModeSwitch.CheckState == CheckState.Checked
            //     ? AltitudeValidationMode.ASL 
            //     : altModeSwitch.CheckState == CheckState.Unchecked
            //     ? AltitudeValidationMode.AGL 
            //     : validationRule.ValidationMode;
            //
            // rulesListView.Items[_selectedRule].SubItems[4].Text = $@"{mode}";
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
                e.Cancel = true; 
            }
            else if (!int.TryParse(altTextBox.Text, out _))
            {
                errorProvider.SetError(altTextBox, "Must be a number");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(altTextBox, ""); 
            }
        }

        private void targetAltTextBox_Validated(object sender, EventArgs e)
        {
            if (_selectedRule < 0)
            {
                return;
            }
            
            // var validationRule = Rules[_selectedRule];
            // validationRule.MaxAlt = Convert.ToInt32(maxAltTextBox.Text);
            // Rules[_selectedRule] = validationRule;
            //
            // rulesListView.Items[_selectedRule].SubItems[1].Text = maxAltTextBox.Text;
        }

        private void maxAltTextBox_Validated(object sender, EventArgs e)
        {
            if (_selectedRule < 0)
            {
                return;
            }
            
            // var validationRule = Rules[_selectedRule];
            // validationRule.TargetAlt = Convert.ToInt32(targetAltTextBox.Text);
            // Rules[_selectedRule] = validationRule;
            //
            // rulesListView.Items[_selectedRule].SubItems[2].Text = targetAltTextBox.Text;
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
            
            // var validationRule = Rules[_selectedRule];
            // validationRule.MinAlt = Convert.ToInt32(minAltTextBox.Text);
            // Rules[_selectedRule] = validationRule;
            //
            // rulesListView.Items[_selectedRule].SubItems[3].Text = minAltTextBox.Text;
        }

        private void minAltTextBox_Validating(object sender, CancelEventArgs e)
        {
            ValidateAlt(e, minAltTextBox);
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

        private void maxAltCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            maxAltTextBox.Enabled = maxAltCheckBox.Checked;
        }

        private void AltitudeValidationParamForm_Shown(object sender, EventArgs e)
        {
            altModeSwitch.BackColor = MaterialManager.Instance.BackgroundColor;
            altModeSwitch.Invalidate(true);
        }
    }

    public enum AltitudeValidationMode
    {
        AGL,
        ASL
    }
}