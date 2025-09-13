using System;
using System.Windows.Forms;

namespace ScreenSaverPlayer
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();

            string? currentPath = SaverConfig.GetVideoPath();
            pathBox.Text = currentPath ?? "";

            soundCheckBox.Checked = SaverConfig.IsSoundEnabled();
            volumeBar.Value = SaverConfig.GetVolume();
        }

        private void BrowseButton_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter =
                "Video Files|*.mp4;*.mkv;*.avi;*.mov;*.flv;*.wmv;*.webm;*.ts;*.ogv;*.m4v;*.3gp;*.f4v;*.vob;*.mpeg;*.mpg|" +
                "All Files|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pathBox.Text = dlg.FileName;
            }
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            string path = pathBox.Text.Trim();
            if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
            {
                SaverConfig.SetVideoPath(path);
                SaverConfig.SetEnableSound(soundCheckBox.Checked);
                SaverConfig.SetVolume(volumeBar.Value);

                MessageBox.Show("Settings saved in registry.", "Configuration");
                Close();
            }
            else
            {
                MessageBox.Show("Please select a valid video file.", "Error");
            }
        }

        private void CancelButton_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
