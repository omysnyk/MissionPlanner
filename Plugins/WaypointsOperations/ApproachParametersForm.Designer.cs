using System.ComponentModel;

namespace MissionActionsPlugin
{
    partial class ApproachParametersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
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
            this.stabilizeSpeedSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            this.approachAltComboBox = new ReaLTaiizor.Controls.MaterialComboBox();
            this.trackerApproachDistTextBox = new ReaLTaiizor.Controls.MaterialTextBox();
            this.cancelButton = new ReaLTaiizor.Controls.MaterialButton();
            this.materialTextBox2 = new ReaLTaiizor.Controls.MaterialTextBox();
            this.okButton = new ReaLTaiizor.Controls.MaterialButton();
            this.changeSpeedDistTextBox1 = new ReaLTaiizor.Controls.MaterialTextBox();
            this.SuspendLayout();
            // 
            // stabilizeSpeedSwitch
            // 
            this.stabilizeSpeedSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.stabilizeSpeedSwitch.Depth = 0;
            this.stabilizeSpeedSwitch.Location = new System.Drawing.Point(57, 201);
            this.stabilizeSpeedSwitch.Margin = new System.Windows.Forms.Padding(0);
            this.stabilizeSpeedSwitch.MouseLocation = new System.Drawing.Point(-1, -1);
            this.stabilizeSpeedSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.stabilizeSpeedSwitch.Name = "stabilizeSpeedSwitch";
            this.stabilizeSpeedSwitch.Ripple = true;
            this.stabilizeSpeedSwitch.Size = new System.Drawing.Size(471, 35);
            this.stabilizeSpeedSwitch.TabIndex = 1;
            this.stabilizeSpeedSwitch.Text = "Change speed before approach point";
            this.stabilizeSpeedSwitch.UseAccentColor = false;
            this.stabilizeSpeedSwitch.UseVisualStyleBackColor = true;
            this.stabilizeSpeedSwitch.CheckedChanged += new System.EventHandler(this.stabilizeSpeedSwitch_CheckedChanged);
            // 
            // approachAltComboBox
            // 
            this.approachAltComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.approachAltComboBox.AutoResize = false;
            this.approachAltComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.approachAltComboBox.Depth = 0;
            this.approachAltComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.approachAltComboBox.DropDownHeight = 174;
            this.approachAltComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.approachAltComboBox.DropDownWidth = 121;
            this.approachAltComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.approachAltComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.approachAltComboBox.FormattingEnabled = true;
            this.approachAltComboBox.Hint = "Approach altitude";
            this.approachAltComboBox.IntegralHeight = false;
            this.approachAltComboBox.ItemHeight = 43;
            this.approachAltComboBox.Items.AddRange(new object[] { "40", "60", "80", "100" });
            this.approachAltComboBox.Location = new System.Drawing.Point(57, 112);
            this.approachAltComboBox.MaxDropDownItems = 4;
            this.approachAltComboBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.approachAltComboBox.Name = "approachAltComboBox";
            this.approachAltComboBox.Size = new System.Drawing.Size(471, 49);
            this.approachAltComboBox.StartIndex = 0;
            this.approachAltComboBox.TabIndex = 2;
            // 
            // trackerApproachDistTextBox
            // 
            this.trackerApproachDistTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.trackerApproachDistTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trackerApproachDistTextBox.Depth = 0;
            this.trackerApproachDistTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.trackerApproachDistTextBox.Hint = "Approach tracking actiavation distance, km";
            this.trackerApproachDistTextBox.Location = new System.Drawing.Point(57, 341);
            this.trackerApproachDistTextBox.MaxLength = 50;
            this.trackerApproachDistTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.trackerApproachDistTextBox.Multiline = false;
            this.trackerApproachDistTextBox.Name = "trackerApproachDistTextBox";
            this.trackerApproachDistTextBox.Size = new System.Drawing.Size(471, 50);
            this.trackerApproachDistTextBox.TabIndex = 3;
            this.trackerApproachDistTextBox.Text = "2.8";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoSize = false;
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.Depth = 0;
            this.cancelButton.DrawShadows = true;
            this.cancelButton.HighEmphasis = false;
            this.cancelButton.Icon = null;
            this.cancelButton.Location = new System.Drawing.Point(261, 428);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cancelButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(120, 60);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.cancelButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.cancelButton.UseAccentColor = false;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // materialTextBox2
            // 
            this.materialTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox2.Depth = 0;
            this.materialTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox2.Location = new System.Drawing.Point(-105, 491);
            this.materialTextBox2.MaxLength = 50;
            this.materialTextBox2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.materialTextBox2.Multiline = false;
            this.materialTextBox2.Name = "materialTextBox2";
            this.materialTextBox2.Size = new System.Drawing.Size(68, 50);
            this.materialTextBox2.TabIndex = 3;
            this.materialTextBox2.Text = "";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.AutoSize = false;
            this.okButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.okButton.Depth = 0;
            this.okButton.DrawShadows = true;
            this.okButton.HighEmphasis = true;
            this.okButton.Icon = null;
            this.okButton.Location = new System.Drawing.Point(408, 428);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.okButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(120, 60);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.TextState = ReaLTaiizor.Controls.MaterialButton.TextStateType.Normal;
            this.okButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.okButton.UseAccentColor = false;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // changeSpeedDistTextBox1
            // 
            this.changeSpeedDistTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.changeSpeedDistTextBox1.Depth = 0;
            this.changeSpeedDistTextBox1.Enabled = false;
            this.changeSpeedDistTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.changeSpeedDistTextBox1.Hint = "Change speed distance, km";
            this.changeSpeedDistTextBox1.Location = new System.Drawing.Point(57, 248);
            this.changeSpeedDistTextBox1.MaxLength = 50;
            this.changeSpeedDistTextBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.changeSpeedDistTextBox1.Multiline = false;
            this.changeSpeedDistTextBox1.Name = "changeSpeedDistTextBox1";
            this.changeSpeedDistTextBox1.ShortcutsEnabled = false;
            this.changeSpeedDistTextBox1.Size = new System.Drawing.Size(471, 50);
            this.changeSpeedDistTextBox1.TabIndex = 4;
            this.changeSpeedDistTextBox1.Text = "10";
            // 
            // ApproachParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(582, 543);
            this.Controls.Add(this.changeSpeedDistTextBox1);
            this.Controls.Add(this.materialTextBox2);
            this.Controls.Add(this.trackerApproachDistTextBox);
            this.Controls.Add(this.approachAltComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.stabilizeSpeedSwitch);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApproachParametersForm";
            this.ShowIcon = false;
            this.Text = "Aproach Parameters";
            this.ResumeLayout(false);
        }

        private ReaLTaiizor.Controls.MaterialTextBox changeSpeedDistTextBox1;

        private ReaLTaiizor.Controls.MaterialButton okButton;
        private ReaLTaiizor.Controls.MaterialSwitch stabilizeSpeedSwitch;
        private ReaLTaiizor.Controls.MaterialComboBox approachAltComboBox;
        private ReaLTaiizor.Controls.MaterialTextBox trackerApproachDistTextBox;
        private ReaLTaiizor.Controls.MaterialButton cancelButton;
        private ReaLTaiizor.Controls.MaterialTextBox materialTextBox2;

        #endregion
    }
}