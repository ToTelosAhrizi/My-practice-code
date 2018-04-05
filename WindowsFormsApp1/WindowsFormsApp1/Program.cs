using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp1
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
            if (File.Exists("C:/Users/Jay/Documents/keys.txt"))
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new Form2());
                Process.Start("C:/Users/Jay/Documents/GitHub/My-practice-code/Generate Key/Generate Key/bin/Debug/Generate Key.exe");
            }
            
        }
    }
}
