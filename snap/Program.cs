using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int SW_SHOWNOMAL = 1;
        private static void HandleRunningInst(Process instance){
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }
        private static Process RunningInst(){
            Process currentProc = Process.GetCurrentProcess();
            Process[] Procs = Process.GetProcessesByName(currentProc.ProcessName);
            foreach(Process _Proc in Procs){
                if(_Proc.Id != currentProc.Id){
                    return _Proc;
                }
            }
            return null;
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isRunning = false;
            Mutex mtx = new Mutex(  true, 
                                    System.Diagnostics.Process.GetCurrentProcess().ProcessName, 
                                    out isRunning);

            if(!isRunning){
                Process _p = RunningInst();
                if(_p != null){
                    HandleRunningInst(_p);
                    Environment.Exit(0);
                }
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
