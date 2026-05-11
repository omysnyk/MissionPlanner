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
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle1 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle2 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle3 = new BrightIdeasSoftware.HeaderStateStyle();
            this.headerFormatStyle1 = new BrightIdeasSoftware.HeaderFormatStyle();
            this.targetAltTextBox = new ReaLTaiizor.Controls.MaterialTextBox();
            this.minAltTextBox = new ReaLTaiizor.Controls.MaterialTextBox();
            this.altModeSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            this.removeRuleButton = new ReaLTaiizor.Controls.MaterialButton();
            this.addRuleButton = new ReaLTaiizor.Controls.MaterialButton();
            this.startWpHeader = new System.Windows.Forms.ColumnHeader();
            this.endWpHeader = new System.Windows.Forms.ColumnHeader();
            this.targetAltHeader = new System.Windows.Forms.ColumnHeader();
            this.minAltHeader = new System.Windows.Forms.ColumnHeader();
            this.altModeHeader = new System.Windows.Forms.ColumnHeader();
            this.rulesListView = new ReaLTaiizor.Controls.MaterialListView();
            this.color = new System.Windows.Forms.ColumnHeader();
            this.waypointsTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.startWaypointTextBox = new ReaLTaiizor.Controls.MaterialTextBox();
            this.endWaypointTextBox = new ReaLTaiizor.Controls.MaterialTextBox();
            this.okButton = new ReaLTaiizor.Controls.MaterialButton();
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
            this.targetAltTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.targetAltTextBox.Depth = 0;
            this.targetAltTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.targetAltTextBox.Hint = "Target Alt";
            this.targetAltTextBox.Location = new System.Drawing.Point(362, 105);
            this.targetAltTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.targetAltTextBox.MaxLength = 50;
            this.targetAltTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.targetAltTextBox.Multiline = false;
            this.targetAltTextBox.Name = "targetAltTextBox";
            this.targetAltTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.targetAltTextBox.Size = new System.Drawing.Size(151, 50);
            this.targetAltTextBox.TabIndex = 8;
            this.targetAltTextBox.Text = "";
            // 
            // minAltTextBox
            // 
            this.minAltTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.minAltTextBox.Depth = 0;
            this.minAltTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.minAltTextBox.Hint = "Min Alt";
            this.minAltTextBox.Location = new System.Drawing.Point(524, 105);
            this.minAltTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.minAltTextBox.MaxLength = 50;
            this.minAltTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.minAltTextBox.Multiline = false;
            this.minAltTextBox.Name = "minAltTextBox";
            this.minAltTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.minAltTextBox.Size = new System.Drawing.Size(146, 50);
            this.minAltTextBox.TabIndex = 8;
            this.minAltTextBox.Text = "";
            // 
            // altModeSwitch
            // 
            this.altModeSwitch.AutoSize = true;
            this.altModeSwitch.Depth = 0;
            this.altModeSwitch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.altModeSwitch.Location = new System.Drawing.Point(684, 105);
            this.altModeSwitch.Margin = new System.Windows.Forms.Padding(0);
            this.altModeSwitch.MouseLocation = new System.Drawing.Point(-1, -1);
            this.altModeSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.altModeSwitch.Name = "altModeSwitch";
            this.altModeSwitch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.altModeSwitch.Ripple = true;
            this.altModeSwitch.Size = new System.Drawing.Size(88, 37);
            this.altModeSwitch.TabIndex = 6;
            this.altModeSwitch.Text = "AGL";
            this.altModeSwitch.UseAccentColor = false;
            this.altModeSwitch.UseVisualStyleBackColor = true;
            this.altModeSwitch.CheckedChanged += new System.EventHandler(this.altModeSelector_CheckedChanged);
            // 
            // removeRuleButton
            // 
            this.removeRuleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeRuleButton.AutoSize = false;
            this.removeRuleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.removeRuleButton.Depth = 0;
            this.removeRuleButton.DrawShadows = true;
            this.removeRuleButton.HighEmphasis = false;
            this.removeRuleButton.Icon = null;
            this.removeRuleButton.Location = new System.Drawing.Point(876, 739);
            this.removeRuleButton.Margin = new System.Windows.Forms.Padding(5);
            this.removeRuleButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.removeRuleButton.Name = "removeRuleButton";
            this.removeRuleButton.Size = new System.Drawing.Size(108, 60);
            this.removeRuleButton.TabIndex = 7;
            this.removeRuleButton.Text = "Clear";
            this.removeRuleButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.removeRuleButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.removeRuleButton.UseAccentColor = false;
            this.removeRuleButton.UseVisualStyleBackColor = true;
            this.removeRuleButton.Click += new System.EventHandler(this.clearRulesButton_Click);
            // 
            // addRuleButton
            // 
            this.addRuleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addRuleButton.AutoSize = false;
            this.addRuleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addRuleButton.Depth = 0;
            this.addRuleButton.DrawShadows = true;
            this.addRuleButton.Enabled = false;
            this.addRuleButton.HighEmphasis = true;
            this.addRuleButton.Icon = null;
            this.addRuleButton.Location = new System.Drawing.Point(993, 105);
            this.addRuleButton.Margin = new System.Windows.Forms.Padding(5);
            this.addRuleButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.addRuleButton.Name = "addRuleButton";
            this.addRuleButton.Size = new System.Drawing.Size(108, 60);
            this.addRuleButton.TabIndex = 7;
            this.addRuleButton.Text = "Add Rule";
            this.addRuleButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.addRuleButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.addRuleButton.UseAccentColor = false;
            this.addRuleButton.UseVisualStyleBackColor = true;
            this.addRuleButton.Click += new System.EventHandler(this.addRuleButton_Click);
            // 
            // startWpHeader
            // 
            this.startWpHeader.Text = "Start";
            this.startWpHeader.Width = 80;
            // 
            // endWpHeader
            // 
            this.endWpHeader.Text = "End";
            this.endWpHeader.Width = 80;
            // 
            // targetAltHeader
            // 
            this.targetAltHeader.Text = "Target Alt";
            this.targetAltHeader.Width = 100;
            // 
            // minAltHeader
            // 
            this.minAltHeader.Text = "Min Alt";
            this.minAltHeader.Width = 100;
            // 
            // altModeHeader
            // 
            this.altModeHeader.Text = "Mode";
            this.altModeHeader.Width = 80;
            // 
            // rulesListView
            // 
            this.rulesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rulesListView.AutoSizeTable = false;
            this.rulesListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rulesListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rulesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.startWpHeader, this.endWpHeader, this.targetAltHeader, this.minAltHeader, this.altModeHeader, this.color });
            this.rulesListView.Depth = 0;
            this.rulesListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rulesListView.FullRowSelect = true;
            this.rulesListView.HideSelection = false;
            this.rulesListView.Location = new System.Drawing.Point(41, 474);
            this.rulesListView.Margin = new System.Windows.Forms.Padding(4);
            this.rulesListView.MinimumSize = new System.Drawing.Size(240, 120);
            this.rulesListView.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rulesListView.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.rulesListView.Name = "rulesListView";
            this.rulesListView.OwnerDraw = true;
            this.rulesListView.Size = new System.Drawing.Size(1061, 224);
            this.rulesListView.TabIndex = 9;
            this.rulesListView.UseCompatibleStateImageBehavior = false;
            this.rulesListView.View = System.Windows.Forms.View.Details;
            this.rulesListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_DrawSubItem);
            // 
            // color
            // 
            this.color.Text = "Color";
            // 
            // waypointsTablePanel
            // 
            this.waypointsTablePanel.ColumnCount = 26;
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.waypointsTablePanel.Location = new System.Drawing.Point(41, 192);
            this.waypointsTablePanel.Margin = new System.Windows.Forms.Padding(4);
            this.waypointsTablePanel.Name = "waypointsTablePanel";
            this.waypointsTablePanel.RowCount = 5;
            this.waypointsTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.waypointsTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.waypointsTablePanel.Size = new System.Drawing.Size(1061, 204);
            this.waypointsTablePanel.TabIndex = 10;
            // 
            // startWaypointTextBox
            // 
            this.startWaypointTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.startWaypointTextBox.Depth = 0;
            this.startWaypointTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.startWaypointTextBox.Hint = "Start WP";
            this.startWaypointTextBox.Location = new System.Drawing.Point(41, 106);
            this.startWaypointTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.startWaypointTextBox.MaxLength = 50;
            this.startWaypointTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.startWaypointTextBox.Multiline = false;
            this.startWaypointTextBox.Name = "startWaypointTextBox";
            this.startWaypointTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.startWaypointTextBox.Size = new System.Drawing.Size(144, 50);
            this.startWaypointTextBox.TabIndex = 11;
            this.startWaypointTextBox.Text = "";
            // 
            // endWaypointTextBox
            // 
            this.endWaypointTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.endWaypointTextBox.Depth = 0;
            this.endWaypointTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.endWaypointTextBox.Hint = "End WP";
            this.endWaypointTextBox.Location = new System.Drawing.Point(193, 105);
            this.endWaypointTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.endWaypointTextBox.MaxLength = 50;
            this.endWaypointTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.endWaypointTextBox.Multiline = false;
            this.endWaypointTextBox.Name = "endWaypointTextBox";
            this.endWaypointTextBox.Size = new System.Drawing.Size(144, 50);
            this.endWaypointTextBox.TabIndex = 12;
            this.endWaypointTextBox.Text = "";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.AutoSize = false;
            this.okButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.okButton.Depth = 0;
            this.okButton.DrawShadows = true;
            this.okButton.Enabled = false;
            this.okButton.HighEmphasis = true;
            this.okButton.Icon = null;
            this.okButton.Location = new System.Drawing.Point(993, 739);
            this.okButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.okButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(108, 60);
            this.okButton.TabIndex = 13;
            this.okButton.Text = "Validate";
            this.okButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.okButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.okButton.UseAccentColor = false;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // AltitudeValidationParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1144, 843);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.endWaypointTextBox);
            this.Controls.Add(this.startWaypointTextBox);
            this.Controls.Add(this.waypointsTablePanel);
            this.Controls.Add(this.removeRuleButton);
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
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ColumnHeader color;

        private ReaLTaiizor.Controls.MaterialButton okButton;

        private ReaLTaiizor.Controls.MaterialTextBox endWaypointTextBox;

        private ReaLTaiizor.Controls.MaterialTextBox startWaypointTextBox;

        private System.Windows.Forms.TableLayoutPanel waypointsTablePanel;

        private ReaLTaiizor.Controls.MaterialSwitch altModeSwitch;
        private ReaLTaiizor.Controls.MaterialButton removeRuleButton;
        private ReaLTaiizor.Controls.MaterialButton addRuleButton;
        private System.Windows.Forms.ColumnHeader startWpHeader;
        private System.Windows.Forms.ColumnHeader endWpHeader;
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