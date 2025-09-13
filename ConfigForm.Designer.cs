namespace ScreenSaverPlayer
{
    partial class ConfigForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox soundCheckBox;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.TrackBar volumeBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            pathBox = new TextBox();
            browseButton = new Button();
            saveButton = new Button();
            cancelButton = new Button();
            soundCheckBox = new CheckBox();
            volumeLabel = new Label();
            volumeBar = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            SuspendLayout();
            // 
            // pathBox
            // 
            pathBox.Location = new Point(12, 12);
            pathBox.Name = "pathBox";
            pathBox.Size = new Size(360, 23);
            pathBox.TabIndex = 0;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(378, 12);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 1;
            browseButton.Text = "Browse...";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += BrowseButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(297, 110);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 2;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(378, 110);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += CancelButton_Click;
            // 
            // soundCheckBox
            // 
            soundCheckBox.Location = new Point(12, 50);
            soundCheckBox.Name = "soundCheckBox";
            soundCheckBox.Size = new Size(150, 24);
            soundCheckBox.TabIndex = 2;
            soundCheckBox.Text = "Enable Sound";
            soundCheckBox.UseVisualStyleBackColor = true;
            // 
            // volumeLabel
            // 
            volumeLabel.Location = new Point(12, 80);
            volumeLabel.Name = "volumeLabel";
            volumeLabel.Size = new Size(80, 23);
            volumeLabel.TabIndex = 1;
            volumeLabel.Text = "Volume:";
            // 
            // volumeBar
            // 
            volumeBar.LargeChange = 10;
            volumeBar.Location = new Point(90, 75);
            volumeBar.Maximum = 100;
            volumeBar.Name = "volumeBar";
            volumeBar.Size = new Size(200, 45);
            volumeBar.TabIndex = 0;
            volumeBar.TickFrequency = 10;
            volumeBar.Value = 50;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(465, 145);
            Controls.Add(volumeBar);
            Controls.Add(volumeLabel);
            Controls.Add(soundCheckBox);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);
            Controls.Add(browseButton);
            Controls.Add(pathBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Screensaver Configuration";
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
