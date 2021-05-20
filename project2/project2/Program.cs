using System;
using System.Windows.Forms;

namespace project2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Startup startup = new Startup();
            Application.Run(startup);
        }
    }
}
