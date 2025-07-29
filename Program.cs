// Program.cs
using System;
using System.Windows.Forms;

namespace Connect2Cart_Desktop_Hub
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayApplicationContext());
        }
    }
}
