using System;
using System.Windows.Forms;
using shopABC.Forms;

namespace shopABC
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FrmMain());
        }
    }
}