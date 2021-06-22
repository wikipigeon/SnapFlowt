using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace snap
{
    public class data
    {
        public data()
        {
            transp = 70;
            visibility = true;
            pip = new Form2();
            pip.Opacity = transp / 100.0;

        }
        public int transp;
        public bool visibility;
        public Form2 pip;

    }

    public static class transp{
        public static Bitmap bt;
        public static bool used = false;
        public static bool touch = false;
        public static string target;
        public static Dictionary<string, data> database = new Dictionary<string, data>();
    }

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
