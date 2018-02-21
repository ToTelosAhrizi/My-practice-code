using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Edit_Files_Pracice
{
    class Program
    {
        static void Main(string[] args)
        {
            string run = "yes";
            while (run != "no")
            {
                UserInfo one = new UserInfo();
                WriteLine("Hello and welcome to the change record console\n\nFor a new Input type Input," +
                          " to veiw a record type View, to edit a record type Edit, to close type Close or Exit");
                string choice = ReadLine();
                if (choice == "Input" || choice == "input")
                {
                    one.UserInput();
                    run = "yes";
                }
                else if (choice == "View" || choice == "view")
                {
                    one.UserOutput();
                    run = "yes";
                }
                else if (choice == "Edit" || choice == "edit")
                {
                    one.UserEdit();
                    run = "yes";
                }
                else if (choice == "Close" || choice == "close" || choice == "Exit" || choice == "exit")
                {
                    WriteLine("Goodybye! (Hit enter to close.)");
                    ReadKey();
                    run = "no";
                }
                else
                {
                    WriteLine("Non valid entry, try again");
                    ReadKey();
                    run = "yes";
                }
            }
        }
    }
    class UserInfo
    {
        string info;
        string name;        
        string rev;
        string change;
        string line;
        public void UserInput()
        {            
            WriteLine("Name of change coordnator:");
            name = ReadLine();

            WriteLine("Revision number (enter 0 if original):");
            rev = ReadLine();
            int.TryParse(rev, out int revision);

            WriteLine("How many lines needed to write the change?");
            line = ReadLine();
            int.TryParse(line, out int lines);
            int startl = 1;
            List<string> changes = new List<string>();
            WriteLine("Enter you changes, make sure you have enough lines to enter");
            while (startl <= lines)
            {
                Write(startl + ". ");
                changes.Add(ReadLine());
                startl++;
            }
            change = string.Join("\n", changes.ToArray());
            FileInfo f = new FileInfo(@"C:\Users\HarperD7\Desktop\name_" + name + "_v_" + revision + ".txt");
            StreamWriter w = f.CreateText();
            string[] infotxt = {"\nInformation listed below.\n\n" ,
                "Name of change coordnator:       " + name,
                "Revision Number:                 " + revision,
                "The changes:                   \n" + change
            };
            info = string.Join("\n", infotxt.ToArray());
            w.WriteLine(info);
            w.Close();
            WriteLine("Success! Hit enter to return to menu.");
            ReadKey();
        }
        public void UserOutput()
        {
            WriteLine("Whose notes do you want to see? Enter their name:");
            name = ReadLine();
            WriteLine("What revision do you want to see?");
            rev = ReadLine();
            int.TryParse(rev, out int revision);
            StreamReader s = File.OpenText(@"C:\Users\HarperD7\Desktop\name_" + name + "_v_" + revision + ".txt");
            string read;
            while((read = s.ReadLine()) != null)
            {
                WriteLine(read);
            }
            s.Close();
            WriteLine("Success! Hit enter to return to menu.");
            ReadKey();
        }
        public void UserEdit()
        {
            WriteLine("Whose notes do you want to change? Enter their name:");
            name = ReadLine();
            WriteLine("What revision do you want to change?");
            rev = ReadLine();
            int.TryParse(rev, out int revision);
            WriteLine("How many lines needed to write the change?");
            line = ReadLine();
            int.TryParse(line, out int lines);
            int startl = 1;
            List<string> changes = new List<string>();
            WriteLine("Enter your name:");
            string namet = ReadLine();
            WriteLine("Enter you changes, make sure you have enough lines to enter");
            while (startl <= lines)
            {
                Write(startl + ". ");
                changes.Add(ReadLine());
                startl++;
            }
            change = string.Join("\n", changes.ToArray());
            int revt = revision+1;
            FileInfo f = new FileInfo(@"C:\Users\HarperD7\Desktop\name_" + name + "_v_" + revision + "_revisedby_" + namet + "_revision_" + revt + ".txt");
            StreamWriter w = f.CreateText();
            string[] infotxt = {"\nInformation listed below.\n\n" ,
                "Name of change coordnator:       " + name,
                "Revision Number:                 " + revision,
                "The changes:                   \n" + change
            };
            info = string.Join("\n", infotxt.ToArray());
            w.WriteLine(info);
            w.Close();
            WriteLine("Success! Hit enter to return to menu.");
            ReadKey();
        }


    }
}
