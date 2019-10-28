using System;
using System.Windows.Forms;

namespace TINY_language_Scanner
{
    static class Program
    {
        static Program()
        {
            CosturaUtility.Initialize();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ScannerForm());
        }
    }
}
