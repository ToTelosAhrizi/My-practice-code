using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ScratchPad4
{
    class Program
    {
        public static int n = 1;
        static void Main(string[] args)
        {
            string again = "yes";
            while (again !="no" && again !="No" && again != "NO" && again != "nO")
            {
                WriteLine("Type 1 to creat a new note or 2 to read a note");
                string choice = ReadLine();
                if (choice == "1")
                {
                    CreateInfo();
                    WriteLine("Again?");
                    again = ReadLine();
                }
                else if (choice == "2")
                {
                    ReadInfo();
                    WriteLine("Again?");
                    again = ReadLine();
                }
                else if (choice == "3")
                {
                    EditInfo();
                    again = ReadLine();
                }
                else
                {
                    WriteLine("Non valid entry, again?");
                    again = ReadLine();
                }
            }
            
        }

        public static void CreateInfo()
        {
            string note = "";
            
            List<string> notes = new List<string>();
            WriteLine("Write your notes, type XXX to finish");
            while (note != "XXX")
            {

                int index = (n);
                Write(n + ". ");
                note = ReadLine();
                if (note != "XXX")
                {
                    notes.Add(note);
                    n++;
                }
                else
                {
                    WriteLine("Ok, all done!\n");
                }

            }
            WriteLine("What do you want to name these notes?");
            string notename = ReadLine();
            string StNotes = string.Join("\n", notes.ToArray());
            string[] noteList = { StNotes };
            System.IO.File.WriteAllLines(@"C:\Users\harperd7\Desktop\Notes on " + notename + ".doc", noteList);
            System.IO.File.WriteAllLines(@"C:\Users\harperd7\Desktop\Notes on " + notename + ".csv", noteList);

        }
        public static void ReadInfo()
        {
            WriteLine("What's the name of the file?");
            string notename = ReadLine();
            string checkingdoc = @"C:\Users\harperd7\Desktop\Notes on " + notename + ".doc";
            string checkingcsv = @"C:\Users\harperd7\Desktop\Notes on " + notename + ".csv";
            if (File.Exists(checkingdoc))
                if (File.Exists(checkingcsv))
                {
                Write(System.IO.File.ReadAllText(@"C:\Users\harperd7\Desktop\Notes on " + notename + ".doc"));
                }
                
            ReadKey();
        }
        public static void EditInfo()
        {
            List<string> notes = new List<string>();
            WriteLine("What's the name of the file?\n");
            string notename = ReadLine();
            
            if (File.Exists(@"C:\Users\harperd7\Desktop\Notes on " + notename + ".doc"))
            {
                string [] mylist = System.IO.File.ReadAllLines(@"C:\Users\harperd7\Desktop\Notes on " + notename + ".doc");                
                string listed = string.Join(n+"\n", mylist.ToArray());
                Write("\n" + listed + "\n");
                notes.AddRange(mylist);
                WriteLine("\nWhat entry would you like to change");
                int.TryParse(ReadLine(), out int line);
                WriteLine("1 to delete the line, 2 to add a change after that line, 3 change the line itself");
                int.TryParse(ReadLine(), out int change);
                if (change == 1)
                {
                    notes.RemoveAt(line - 1);
                    string nlist = string.Join("\n", notes.ToArray());
                    Write("\n" + nlist);
                    string StNotes = string.Join("\n", notes.ToArray());
                    string[] noteList = { StNotes };
                    System.IO.File.WriteAllLines(@"C:\Users\harperd7\Desktop\Notes on " + notename + ".doc", noteList);
                    ReadKey();
                }
                else if (change == 2)
                {
                    WriteLine("What would you like to add?");
                    notes.Insert(line, ReadLine());
                    string nlist = string.Join("\n", notes.ToArray());
                    Write("\n" + nlist);
                    string StNotes = string.Join("\n", notes.ToArray());
                    string[] noteList = { StNotes };
                    System.IO.File.WriteAllLines(@"C:\Users\harperd7\Desktop\Notes on " + notename + ".doc", noteList);
                    ReadKey();
                }
                else if (change == 3)
                {
                    WriteLine("What would you like to change it to?");
                    notes.Insert(line, ReadLine());
                    notes.RemoveAt(line - 1);
                    string nlist = string.Join("\n", notes.ToArray());
                    Write("\n" + nlist);
                    string StNotes = string.Join("\n", notes.ToArray());
                    string[] noteList = { StNotes };
                    System.IO.File.WriteAllLines(@"C:\Users\harperd7\Desktop\Notes on " + notename + ".doc", noteList);
                    ReadKey();
                }
                else
                {
                    WriteLine("Non Valid");
                }



            }
            else
            {
                WriteLine("Does not exist");
            }
           


        }
    }
}
