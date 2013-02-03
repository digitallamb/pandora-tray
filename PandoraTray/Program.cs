using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PandoraTray
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

            ApplicationContext applicationContext = new SystemTrayApplicationContext();
            Application.Run(applicationContext);
        }
    }
}


