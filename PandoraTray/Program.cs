using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PandoraTray
{
    static class Program
    {
        private const string APPLICATION_GUID = "3C2C83C6-7800-11E2-BEF9-B8E36188709B";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (System.Threading.Mutex singleInstanceMutex = new System.Threading.Mutex(false, APPLICATION_GUID))
            {
                if (singleInstanceMutex.WaitOne(0, false))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    ApplicationContext applicationContext = new SystemTrayApplicationContext();
                    GC.Collect();
                    Application.Run(applicationContext);
                }
                else
                {
                    return;
                }
            }
        }
    }
}


