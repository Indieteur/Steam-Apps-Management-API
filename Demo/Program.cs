using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    static class Program
    {
        public static bool failedStartMain = false; //This variable will let us know if we need to launch the frmDemoVDF on its own.
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMainDemo());
            if (failedStartMain) //If the loading of the Steam Apps Manager library has failed, then launch frmDemoVDF instead.
                Application.Run(new frmDemoVDF());
        }
    }
}
