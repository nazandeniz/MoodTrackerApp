using System;
using System.Threading;
using System.Windows.Forms;

namespace MoodTrackerApp.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Uygulama geneli exception yakalayýcýlar
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show(e.Exception.ToString(), "UI Thread Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                MessageBox.Show(ex?.ToString() ?? "Unknown fatal error", "Non-UI Thread Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
