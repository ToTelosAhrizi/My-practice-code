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
using System.Text.RegularExpressions;
using static System.Console;
using System.Net;
using System.Net.Mail;

namespace trying_gui_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string [] notes = richTextBox1.Lines;
            string note_name = textBox1.Text;
            File.WriteAllLines("C:/Users/HarperD7/Documents/" + note_name + ".txt", notes);
            richTextBox1.Lines = Array.Empty<string>();
            textBox1.Text = String.Empty;
            
        }
        //C:\Users\HarperD7\Documents
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string form_name = textBox2.Text; 
            if (File.Exists("C:/Users/HarperD7/Documents/" + form_name + ".txt"))
            {
                string[] notes = File.ReadAllLines("C:/Users/HarperD7/Documents/" + form_name + ".txt");
                richTextBox1.Lines = notes;
            }
            else
            {
                richTextBox1.Text = "That format doesn't exist, sorry";
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
         
            string[] filelist = (Directory.GetFiles("C:/Users/HarperD7/Documents/", "*.txt"));
            List<string> hi = new List<string>();
            foreach (string o in filelist)
            {

                hi.Add(Path.GetFileNameWithoutExtension(o) + ".txt");
               
            }
            string fl = string.Join("\n", hi.ToArray());
            richTextBox2.Text = fl;
            richTextBox2.ReadOnly = true;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
