using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace snap
{
    static class Program
    {
        public static EventWaitHandle procStarted;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isRunning = false;
            procStarted = new EventWaitHandle(  false, 
                                                EventResetMode.AutoReset, "damnOne",
                                                out isRunning);

            if(!isRunning){
                procStarted.Set();
                return ;
            }


            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
