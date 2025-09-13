using System;
using System.Windows.Forms;
using ScreenSaverPlayer;

namespace ScreenSaverPlayer
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string firstArgument = args.Length > 0 ? args[0].ToLowerInvariant() : "/s";

            if (firstArgument.StartsWith("/c"))
            {
                Application.Run(new ConfigForm());
            }
            else if (firstArgument.StartsWith("/p"))
            {
                IntPtr previewWnd = IntPtr.Zero;
                if (args.Length > 1 && long.TryParse(args[1], out long hwnd))
                    previewWnd = new IntPtr(hwnd);

                Application.Run(new SaverForm(previewWnd, true));
            }
            else // /s fullscreen
            {
                foreach (var screen in Screen.AllScreens)
                {
                    var form = new SaverForm(IntPtr.Zero, false)
                    {
                        StartPosition = FormStartPosition.Manual,
                        Bounds = screen.Bounds
                    };
                    form.Show();
                }

                Application.Run();
            }
        }
    }
}
