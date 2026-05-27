namespace MissionActionsPlugin
{
    partial class AltitudeValidationParamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle1 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle2 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle3 = new BrightIdeasSoftware.HeaderStateStyle();
            this.headerFormatStyle1 = new BrightIdeasSoftware.HeaderFormatStyle();
            this.targetAltTextBox = new ReaLTaiizor.Controls.MaterialTextBox();
            this.minAltTextBox = new ReaLTaiizor.Controls.MaterialTextBox();
            this.altModeSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            this.clearRuleAssignmentsButton = new ReaLTaiizor.Controls.MaterialButton();
            this.addRuleButton = new ReaLTaiizor.Controls.MaterialButton();
            this.targetAltHeader = new System.Windows.Forms.ColumnHeader();
            this.minAltHeader = new System.Windows.Forms.ColumnHeader();
            this.altModeHeader = new System.Windows.Forms.ColumnHeader();
            this.rulesListView = new ReaLTaiizor.Controls.MaterialListView();
            this.ruleNumColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.maxAltHeader = new System.Windows.Forms.ColumnHeader();
            this.color = new System.Windows.Forms.ColumnHeader();
            this.validateButton = new ReaLTaiizor.Controls.MaterialButton();
            this.maxAltTextBox = new ReaLTaiizor.Controls.MaterialTextBox();
            this.assignRuleButton = new ReaLTaiizor.Controls.MaterialButton();
            this.deleteButton = new ReaLTaiizor.Controls.MaterialButton();
            this.segmentStartWPComboBox = new ReaLTaiizor.Controls.MaterialComboBox();
            this.segmentEndWPComboBox = new ReaLTaiizor.Controls.MaterialComboBox();
            this.rulesAssignmentsListView = new ReaLTaiizor.Controls.MaterialListView();
            this.startWpColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.enWpColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.ruleColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.ruleColorColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.maxAltCheckBox = new ReaLTaiizor.Controls.MaterialCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // headerFormatStyle1
            // 
            this.headerFormatStyle1.Hot = headerStateStyle1;
            this.headerFormatStyle1.Normal = headerStateStyle2;
            this.headerFormatStyle1.Pressed = headerStateStyle3;
            // 
            // targetAltTextBox
            // 
            this.targetAltTextBox.AutoWordSelection = true;
            this.targetAltTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.targetAltTextBox.Depth = 0;
            this.targetAltTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.targetAltTextBox.Hint = "Target Alt";
            this.targetAltTextBox.Location = new System.Drawing.Point(268, 118);
            this.targetAltTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.targetAltTextBox.MaxLength = 50;
            this.targetAltTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.targetAltTextBox.Multiline = false;
            this.targetAltTextBox.Name = "targetAltTextBox";
            this.targetAltTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.targetAltTextBox.Size = new System.Drawing.Size(152, 50);
            this.targetAltTextBox.TabIndex = 1;
            this.targetAltTextBox.Text = "";
            this.targetAltTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.targetAltTextBox_Validating);
            this.targetAltTextBox.Validated += new System.EventHandler(this.targetAltTextBox_Validated);
            // 
            // minAltTextBox
            // 
            this.minAltTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.minAltTextBox.Depth = 0;
            this.minAltTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.minAltTextBox.Hint = "Min Alt";
            this.minAltTextBox.Location = new System.Drawing.Point(442, 118);
            this.minAltTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.minAltTextBox.MaxLength = 50;
            this.minAltTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.minAltTextBox.Multiline = false;
            this.minAltTextBox.Name = "minAltTextBox";
            this.minAltTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.minAltTextBox.Size = new System.Drawing.Size(146, 50);
            this.minAltTextBox.TabIndex = 3;
            this.minAltTextBox.Text = "";
            this.minAltTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.minAltTextBox_Validating);
            this.minAltTextBox.Validated += new System.EventHandler(this.minAltTextBox_Validated);
            // 
            // altModeSwitch
            // 
            this.altModeSwitch.AutoSize = true;
            this.altModeSwitch.Depth = 0;
            this.altModeSwitch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.altModeSwitch.Location = new System.Drawing.Point(610, 118);
            this.altModeSwitch.Margin = new System.Windows.Forms.Padding(0);
            this.altModeSwitch.MouseLocation = new System.Drawing.Point(-1, -1);
            this.altModeSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.altModeSwitch.Name = "altModeSwitch";
            this.altModeSwitch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.altModeSwitch.Ripple = true;
            this.altModeSwitch.Size = new System.Drawing.Size(88, 37);
            this.altModeSwitch.TabIndex = 4;
            this.altModeSwitch.Text = "AGL";
            this.altModeSwitch.UseAccentColor = false;
            this.altModeSwitch.UseVisualStyleBackColor = true;
            this.altModeSwitch.CheckedChanged += new System.EventHandler(this.altModeSelector_CheckedChanged);
            // 
            // clearRuleAssignmentsButton
            // 
            this.clearRuleAssignmentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearRuleAssignmentsButton.AutoSize = false;
            this.clearRuleAssignmentsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clearRuleAssignmentsButton.Depth = 0;
            this.clearRuleAssignmentsButton.DrawShadows = true;
            this.clearRuleAssignmentsButton.HighEmphasis = false;
            this.clearRuleAssignmentsButton.Icon = null;
            this.clearRuleAssignmentsButton.Location = new System.Drawing.Point(838, 982);
            this.clearRuleAssignmentsButton.Margin = new System.Windows.Forms.Padding(6, 9, 6, 9);
            this.clearRuleAssignmentsButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.clearRuleAssignmentsButton.Name = "clearRuleAssignmentsButton";
            this.clearRuleAssignmentsButton.Size = new System.Drawing.Size(108, 60);
            this.clearRuleAssignmentsButton.TabIndex = 11;
            this.clearRuleAssignmentsButton.Text = "Clear";
            this.clearRuleAssignmentsButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.clearRuleAssignmentsButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.clearRuleAssignmentsButton.UseAccentColor = false;
            this.clearRuleAssignmentsButton.UseVisualStyleBackColor = true;
            this.clearRuleAssignmentsButton.Click += new System.EventHandler(this.clearRuleAssignmentsButton_Click);
            // 
            // addRuleButton
            // 
            this.addRuleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addRuleButton.AutoSize = false;
            this.addRuleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addRuleButton.Depth = 0;
            this.addRuleButton.DrawShadows = true;
            this.addRuleButton.HighEmphasis = true;
            this.addRuleButton.Icon = null;
            this.addRuleButton.Location = new System.Drawing.Point(957, 118);
            this.addRuleButton.Margin = new System.Windows.Forms.Padding(6, 9, 6, 9);
            this.addRuleButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.addRuleButton.Name = "addRuleButton";
            this.addRuleButton.Size = new System.Drawing.Size(108, 60);
            this.addRuleButton.TabIndex = 5;
            this.addRuleButton.Text = "Add Rule";
            this.addRuleButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.addRuleButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.addRuleButton.UseAccentColor = false;
            this.addRuleButton.UseVisualStyleBackColor = true;
            this.addRuleButton.Click += new System.EventHandler(this.addRuleButton_Click);
            // 
            // targetAltHeader
            // 
            this.targetAltHeader.Text = "Target Alt";
            this.targetAltHeader.Width = 178;
            // 
            // minAltHeader
            // 
            this.minAltHeader.Text = "Min Alt";
            this.minAltHeader.Width = 100;
            // 
            // altModeHeader
            // 
            this.altModeHeader.Text = "Mode";
            this.altModeHeader.Width = 109;
            // 
            // rulesListView
            // 
            this.rulesListView.AllowDrop = true;
            this.rulesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rulesListView.AutoSizeTable = false;
            this.rulesListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rulesListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rulesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.ruleNumColumnHeader, this.maxAltHeader, this.targetAltHeader, this.minAltHeader, this.altModeHeader, this.color });
            this.rulesListView.Depth = 0;
            this.rulesListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rulesListView.FullRowSelect = true;
            this.rulesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.rulesListView.HideSelection = false;
            this.rulesListView.Location = new System.Drawing.Point(40, 212);
            this.rulesListView.Margin = new System.Windows.Forms.Padding(4);
            this.rulesListView.MinimumSize = new System.Drawing.Size(240, 120);
            this.rulesListView.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rulesListView.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.rulesListView.MultiSelect = false;
            this.rulesListView.Name = "rulesListView";
            this.rulesListView.OwnerDraw = true;
            this.rulesListView.Size = new System.Drawing.Size(1024, 262);
            this.rulesListView.TabIndex = 8;
            this.rulesListView.UseCompatibleStateImageBehavior = false;
            this.rulesListView.View = System.Windows.Forms.View.Details;
            this.rulesListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_DrawSubItem);
            this.rulesListView.SelectedIndexChanged += new System.EventHandler(this.rulesListView_SelectedIndexChanged);
            // 
            // ruleNumColumnHeader
            // 
            this.ruleNumColumnHeader.Text = "#";
            this.ruleNumColumnHeader.Width = 40;
            // 
            // maxAltHeader
            // 
            this.maxAltHeader.Text = "Max Alt";
            this.maxAltHeader.Width = 137;
            // 
            // color
            // 
            this.color.Text = "Color";
            this.color.Width = 109;
            // 
            // validateButton
            // 
            this.validateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.validateButton.AutoSize = false;
            this.validateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.validateButton.Depth = 0;
            this.validateButton.DrawShadows = true;
            this.validateButton.Enabled = false;
            this.validateButton.HighEmphasis = true;
            this.validateButton.Icon = null;
            this.validateButton.Location = new System.Drawing.Point(956, 982);
            this.validateButton.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.validateButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(108, 60);
            this.validateButton.TabIndex = 10;
            this.validateButton.Text = "Validate";
            this.validateButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.validateButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.validateButton.UseAccentColor = false;
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // maxAltTextBox
            // 
            this.maxAltTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maxAltTextBox.Depth = 0;
            this.maxAltTextBox.Enabled = false;
            this.maxAltTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.maxAltTextBox.Hint = "Max Alt";
            this.maxAltTextBox.Location = new System.Drawing.Point(102, 118);
            this.maxAltTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.maxAltTextBox.MaxLength = 50;
            this.maxAltTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.maxAltTextBox.Multiline = false;
            this.maxAltTextBox.Name = "maxAltTextBox";
            this.maxAltTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.maxAltTextBox.Size = new System.Drawing.Size(146, 50);
            this.maxAltTextBox.TabIndex = 2;
            this.maxAltTextBox.Text = "";
            this.maxAltTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.maxAltTextBox_Validating);
            this.maxAltTextBox.Validated += new System.EventHandler(this.maxAltTextBox_Validated);
            // 
            // assignRuleButton
            // 
            this.assignRuleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.assignRuleButton.AutoSize = false;
            this.assignRuleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.assignRuleButton.Depth = 0;
            this.assignRuleButton.DrawShadows = true;
            this.assignRuleButton.Enabled = false;
            this.assignRuleButton.HighEmphasis = true;
            this.assignRuleButton.Icon = null;
            this.assignRuleButton.Location = new System.Drawing.Point(956, 504);
            this.assignRuleButton.Margin = new System.Windows.Forms.Padding(6, 9, 6, 9);
            this.assignRuleButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.assignRuleButton.Name = "assignRuleButton";
            this.assignRuleButton.Size = new System.Drawing.Size(108, 60);
            this.assignRuleButton.TabIndex = 9;
            this.assignRuleButton.Text = "Assign";
            this.assignRuleButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.assignRuleButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.assignRuleButton.UseAccentColor = false;
            this.assignRuleButton.UseVisualStyleBackColor = true;
            this.assignRuleButton.Click += new System.EventHandler(this.assignRuleButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.AutoSize = false;
            this.deleteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteButton.Depth = 0;
            this.deleteButton.DrawShadows = true;
            this.deleteButton.HighEmphasis = false;
            this.deleteButton.Icon = null;
            this.deleteButton.Location = new System.Drawing.Point(838, 504);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(6, 9, 6, 9);
            this.deleteButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(108, 60);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete";
            this.deleteButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.deleteButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.deleteButton.UseAccentColor = false;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.clearRulesButton_Click);
            // 
            // segmentStartWPComboBox
            // 
            this.segmentStartWPComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.segmentStartWPComboBox.AutoResize = false;
            this.segmentStartWPComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.segmentStartWPComboBox.Depth = 0;
            this.segmentStartWPComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.segmentStartWPComboBox.DropDownHeight = 174;
            this.segmentStartWPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.segmentStartWPComboBox.DropDownWidth = 121;
            this.segmentStartWPComboBox.Enabled = false;
            this.segmentStartWPComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.segmentStartWPComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.segmentStartWPComboBox.FormattingEnabled = true;
            this.segmentStartWPComboBox.Hint = "Start WP";
            this.segmentStartWPComboBox.IntegralHeight = false;
            this.segmentStartWPComboBox.ItemHeight = 43;
            this.segmentStartWPComboBox.Location = new System.Drawing.Point(40, 504);
            this.segmentStartWPComboBox.MaxDropDownItems = 4;
            this.segmentStartWPComboBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.segmentStartWPComboBox.Name = "segmentStartWPComboBox";
            this.segmentStartWPComboBox.Size = new System.Drawing.Size(228, 49);
            this.segmentStartWPComboBox.StartIndex = 0;
            this.segmentStartWPComboBox.TabIndex = 12;
            this.segmentStartWPComboBox.SelectedIndexChanged += new System.EventHandler(this.segmentStartWPComboBox_SelectedIndexChanged);
            // 
            // segmentEndWPComboBox
            // 
            this.segmentEndWPComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.segmentEndWPComboBox.AutoResize = false;
            this.segmentEndWPComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.segmentEndWPComboBox.Depth = 0;
            this.segmentEndWPComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.segmentEndWPComboBox.DropDownHeight = 174;
            this.segmentEndWPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.segmentEndWPComboBox.DropDownWidth = 121;
            this.segmentEndWPComboBox.Enabled = false;
            this.segmentEndWPComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.segmentEndWPComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.segmentEndWPComboBox.FormattingEnabled = true;
            this.segmentEndWPComboBox.Hint = "End WP";
            this.segmentEndWPComboBox.IntegralHeight = false;
            this.segmentEndWPComboBox.ItemHeight = 43;
            this.segmentEndWPComboBox.Location = new System.Drawing.Point(290, 504);
            this.segmentEndWPComboBox.MaxDropDownItems = 4;
            this.segmentEndWPComboBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.segmentEndWPComboBox.Name = "segmentEndWPComboBox";
            this.segmentEndWPComboBox.Size = new System.Drawing.Size(228, 49);
            this.segmentEndWPComboBox.StartIndex = 0;
            this.segmentEndWPComboBox.TabIndex = 13;
            this.segmentEndWPComboBox.SelectedIndexChanged += new System.EventHandler(this.segmentEndWPComboBox_SelectedIndexChanged);
            // 
            // rulesAssignmentsListView
            // 
            this.rulesAssignmentsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rulesAssignmentsListView.AutoSizeTable = false;
            this.rulesAssignmentsListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rulesAssignmentsListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rulesAssignmentsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.startWpColumnHeader, this.enWpColumnHeader, this.ruleColumnHeader, this.ruleColorColumnHeader });
            this.rulesAssignmentsListView.Depth = 0;
            this.rulesAssignmentsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rulesAssignmentsListView.FullRowSelect = true;
            this.rulesAssignmentsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.rulesAssignmentsListView.HideSelection = false;
            this.rulesAssignmentsListView.Location = new System.Drawing.Point(40, 597);
            this.rulesAssignmentsListView.Margin = new System.Windows.Forms.Padding(4);
            this.rulesAssignmentsListView.MinimumSize = new System.Drawing.Size(240, 120);
            this.rulesAssignmentsListView.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rulesAssignmentsListView.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.rulesAssignmentsListView.MultiSelect = false;
            this.rulesAssignmentsListView.Name = "rulesAssignmentsListView";
            this.rulesAssignmentsListView.OwnerDraw = true;
            this.rulesAssignmentsListView.Size = new System.Drawing.Size(1024, 357);
            this.rulesAssignmentsListView.TabIndex = 14;
            this.rulesAssignmentsListView.UseCompatibleStateImageBehavior = false;
            this.rulesAssignmentsListView.View = System.Windows.Forms.View.Details;
            this.rulesAssignmentsListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_DrawSubItem);
            // 
            // startWpColumnHeader
            // 
            this.startWpColumnHeader.Text = "Start waypoint";
            this.startWpColumnHeader.Width = 137;
            // 
            // enWpColumnHeader
            // 
            this.enWpColumnHeader.Text = "End waypoint";
            this.enWpColumnHeader.Width = 178;
            // 
            // ruleColumnHeader
            // 
            this.ruleColumnHeader.Text = "Rule";
            this.ruleColumnHeader.Width = 255;
            // 
            // ruleColorColumnHeader
            // 
            this.ruleColorColumnHeader.Text = "Color";
            this.ruleColorColumnHeader.Width = 109;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // maxAltCheckBox
            // 
            this.maxAltCheckBox.Depth = 0;
            this.maxAltCheckBox.Location = new System.Drawing.Point(40, 126);
            this.maxAltCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.maxAltCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.maxAltCheckBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.maxAltCheckBox.Name = "maxAltCheckBox";
            this.maxAltCheckBox.Ripple = true;
            this.maxAltCheckBox.Size = new System.Drawing.Size(56, 56);
            this.maxAltCheckBox.TabIndex = 16;
            this.maxAltCheckBox.UseVisualStyleBackColor = true;
            this.maxAltCheckBox.CheckedChanged += new System.EventHandler(this.maxAltCheckBox_CheckedChanged);
            // 
            // AltitudeValidationParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1107, 1086);
            this.Controls.Add(this.maxAltCheckBox);
            this.Controls.Add(this.rulesAssignmentsListView);
            this.Controls.Add(this.segmentEndWPComboBox);
            this.Controls.Add(this.segmentStartWPComboBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.assignRuleButton);
            this.Controls.Add(this.maxAltTextBox);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.clearRuleAssignmentsButton);
            this.Controls.Add(this.addRuleButton);
            this.Controls.Add(this.rulesListView);
            this.Controls.Add(this.altModeSwitch);
            this.Controls.Add(this.minAltTextBox);
            this.Controls.Add(this.targetAltTextBox);
            this.DrawerHideTabName = new string[0];
            this.DrawerNonClickTabPage = new System.Windows.Forms.TabPage[0];
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AltitudeValidationParamForm";
            this.Padding = new System.Windows.Forms.Padding(4, 36, 4, 2);
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Altitude validation parameters";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private ReaLTaiizor.Controls.MaterialCheckBox maxAltCheckBox;

        private System.Windows.Forms.ErrorProvider errorProvider;

        private System.Windows.Forms.ColumnHeader ruleNumColumnHeader;

        private ReaLTaiizor.Controls.MaterialListView rulesAssignmentsListView;
        private System.Windows.Forms.ColumnHeader startWpColumnHeader;
        private System.Windows.Forms.ColumnHeader enWpColumnHeader;
        private System.Windows.Forms.ColumnHeader ruleColumnHeader;
        private System.Windows.Forms.ColumnHeader ruleColorColumnHeader;

        private ReaLTaiizor.Controls.MaterialComboBox segmentStartWPComboBox;
        private ReaLTaiizor.Controls.MaterialComboBox segmentEndWPComboBox;

        private System.Windows.Forms.ColumnHeader maxAltHeader;

        private ReaLTaiizor.Controls.MaterialButton deleteButton;

        private ReaLTaiizor.Controls.MaterialTextBox maxAltTextBox;
        private ReaLTaiizor.Controls.MaterialButton assignRuleButton;

        private System.Windows.Forms.ColumnHeader color;

        private ReaLTaiizor.Controls.MaterialButton validateButton;

        private ReaLTaiizor.Controls.MaterialSwitch altModeSwitch;
        private ReaLTaiizor.Controls.MaterialButton clearRuleAssignmentsButton;
        private ReaLTaiizor.Controls.MaterialButton addRuleButton;
        private System.Windows.Forms.ColumnHeader targetAltHeader;
        private System.Windows.Forms.ColumnHeader minAltHeader;
        private System.Windows.Forms.ColumnHeader altModeHeader;
        private ReaLTaiizor.Controls.MaterialListView rulesListView;

        #endregion
        private BrightIdeasSoftware.HeaderFormatStyle headerFormatStyle1;
        private ReaLTaiizor.Controls.MaterialTextBox targetAltTextBox;
        private ReaLTaiizor.Controls.MaterialTextBox minAltTextBox;
    }
}