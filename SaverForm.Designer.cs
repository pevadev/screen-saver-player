namespace ScreenSaverPlayer
{
    partial class SaverForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel videoPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaverForm));
            videoPanel = new Panel();
            SuspendLayout();
            // 
            // videoPanel
            // 
            videoPanel.BackColor = Color.Black;
            videoPanel.Dock = DockStyle.Fill;
            videoPanel.Location = new Point(0, 0);
            videoPanel.Name = "videoPanel";
            videoPanel.Size = new Size(800, 450);
            videoPanel.TabIndex = 0;
            // 
            // SaverForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(videoPanel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SaverForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Screensaver";
            ResumeLayout(false);
        }

        #endregion
    }
}
