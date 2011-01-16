using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestCrypt
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // register an exception handler which will handle all exceptions not directly handled within the program:
            // without the exception handler the program will terminate silently and no one knows why
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        /// <summary>
        /// The exception handler which will handle all exceptions not directly handled within the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                MessageBox.Show(string.Format("{1}{0}Stack Trace:{0}{2}", Environment.NewLine, ex.Message, ex.StackTrace), "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
