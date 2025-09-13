using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace ScreenSaverPlayer
{
    public partial class SaverForm : Form
    {
        private static List<SaverForm> ActiveForms = new List<SaverForm>();
        private static bool _closingAll = false;
        private static readonly object _soundLock = new object();
        private static bool _soundFormCreated = false;

        private LibVLC _VLC;
        private MediaPlayer _mediaPlayer;
        private VideoView _videoView;
        private Point _lastMousePosition = Point.Empty;
        private bool _previewMode;
        private Random _random = new Random();

        // Win32 interop for preview mode
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private const int GWL_STYLE = -16;
        private const int WS_CHILD = 0x40000000;

        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);
        }

        public SaverForm(IntPtr previewHandle, bool previewMode)
        {
            InitializeComponent();

            _previewMode = previewMode;

            string? videoPath = SaverConfig.GetVideoPath();
            if (string.IsNullOrEmpty(videoPath) || !System.IO.File.Exists(videoPath))
            {
                if (!_previewMode)
                    MessageBox.Show("No valid video configured. Please run configuration first.", "Screensaver");
                Close();
                return;
            }

            // Determine if this form should play sound
            bool enableSound = false;
            lock (_soundLock)
            {
                if (SaverConfig.IsSoundEnabled() && !_soundFormCreated)
                {
                    enableSound = true;
                    _soundFormCreated = true;
                }
            }

            // Initialize LibVLC
            _VLC = enableSound
                ? new LibVLC("--quiet", "--loop")                     // primary audio form
                : new LibVLC("--quiet", "--loop", "--no-audio");     // secondary forms

            ActiveForms.Add(this);

            // Create VideoView dynamically
            _videoView = new VideoView { Dock = DockStyle.Fill };
            videoPanel.Controls.Add(_videoView);

            _mediaPlayer = new MediaPlayer(_VLC)
            {
                Mute = false,
                Volume = SaverConfig.GetVolume(),
                AspectRatio = null, // native aspect
                Scale = 0           // auto-scale
            };

            // Attach MediaPlayer AFTER configuring volume/mute
            _videoView.MediaPlayer = _mediaPlayer;

            // this.SizeChanged += VideoView_SizeChanged;
            _videoView.Resize += (_, __) => ApplyStretchAspect();
            _videoView.MouseMove += SaverForm_MouseMove;

            if (_previewMode && previewHandle != IntPtr.Zero)
            {
                // embed in preview window
                SetParent(this.Handle, previewHandle);
                int style = GetWindowLong(this.Handle, GWL_STYLE);
                SetWindowLong(this.Handle, GWL_STYLE, style | WS_CHILD);

                NativeMethods.GetClientRect(previewHandle, out Rectangle rect);
                this.Size = rect.Size;
                this.Location = new Point(0, 0);
                this.Dock = DockStyle.Fill;
            }
            else
            {
                // fullscreen mode
                FormBorderStyle = FormBorderStyle.None;
                Bounds = Screen.FromControl(this).Bounds;
                TopMost = true;
                Cursor.Hide();

                KeyPreview = true;
                Focus();

                KeyDown += SaverForm_KeyDown;
                MouseMove += SaverForm_MouseMove;
                MouseClick += (_, __) => CloseApp();
                _lastMousePosition = Cursor.Position;
            }

            // Play media
            var media = new Media(_VLC, videoPath, FromType.FromPath);
            media.AddOption(":input-repeat=-1");
            _mediaPlayer.Play(media);

            // Start video at random position
            _mediaPlayer.Position = (float)_random.NextDouble();

            _mediaPlayer.EndReached += (sender, e) =>
            {
                if (_mediaPlayer == null) return;

                Task.Delay(10).ContinueWith(_ =>
                {
                    if (_mediaPlayer != null)
                        _mediaPlayer.Play(media);
                });
            };

            VideoView_SizeChanged(null, null);
        }

        private void ApplyStretchAspect()
        {
            if (_mediaPlayer == null || _videoView == null) return;

            int w = Math.Max(1, _videoView.ClientSize.Width);
            int h = Math.Max(1, _videoView.ClientSize.Height);

            // Stretch to fill the control (distorts to match control aspect)
            _mediaPlayer.AspectRatio = $"{w}:{h}";
            _mediaPlayer.Scale = 0; // let VLC fit to the AspectRatio above
        }

        private void VideoView_SizeChanged(object? sender, EventArgs e)
        {
            if (_mediaPlayer == null || !_mediaPlayer.IsPlaying)
                return;

            uint videoW = 0, videoH = 0;
            if (!_mediaPlayer.Size(0, ref videoW, ref videoH) || videoH == 0)
                return;

            var rect = _videoView.Parent.ClientRectangle;
            float aspectRatio = (float)videoW / videoH;

            // Always match height
            int newHeight = rect.Height;
            int newWidth = (int)(newHeight * aspectRatio);

            int newLeft = (rect.Width - newWidth) / 2;

            // Oversize the control crop happens automatically
            _videoView.SetBounds(newLeft, 0, newWidth, newHeight);

            // Tell VLC to fill the control (ignore its own aspect)
            _mediaPlayer.AspectRatio = $"{newWidth}:{newHeight}";
            _mediaPlayer.Scale = 0;
        }

        private void SaverForm_MouseMove(object? sender, MouseEventArgs e)
        {
            CloseApp();
        }

        private void SaverForm_KeyDown(object? sender, KeyEventArgs e)
        {
            CloseApp();
        }

        private static void CloseApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }   
    }
}
