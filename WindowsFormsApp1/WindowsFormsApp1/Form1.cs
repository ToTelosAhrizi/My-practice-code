using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string maca = string.Empty;
            NetworkInterface[] lookfornic = NetworkInterface.GetAllNetworkInterfaces();
            foreach(NetworkInterface nic in lookfornic)
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    maca += nic.GetPhysicalAddress().ToString();
                }
                
            }
            
            List<string> validkeys = new List<string>(File.ReadAllLines(@"C:/Users/Jay/Documents/keys.txt"));
           if (!(validkeys.Contains(maca)))
            {
                int entry = 0;
                while (!(validkeys.Contains(maca)))
                {

                    if (validkeys.ElementAt(entry).Count() != 12)
                    {
                        validkeys.Insert(entry, maca);
                    }
                    else
                    {
                        entry++;
                    }
                }
                File.WriteAllLines(@"C:/Users/Jay/Documents/keys.txt", validkeys);
                richTextBox1.Text = "You're good to go!";

            }
            else
            {
                richTextBox1.Text = "You're great!";
            }
            
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
