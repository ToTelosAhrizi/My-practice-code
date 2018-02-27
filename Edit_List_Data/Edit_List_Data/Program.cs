using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Changing_List_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> practice = new List<int>();
            practice.Add(0);
            int start = 1;
            int x = 1;
            WriteLine("How many entries?(Non Valid entries will close.)");
            if (int.TryParse(ReadLine(), out int entries))
            {
                while (start <= entries)
                {
                    practice.Add(x);
                    x++;
                    start++;
                }
                string p = string.Join("\n", practice.ToArray());
                WriteLine(p);
                WriteLine("\nWhat entry would you like to change");
                int.TryParse(ReadLine(), out int change);
                WriteLine("\nPress 1 to delete, 2 to update, 3 to add an entry. All else will close.");
                int.TryParse(ReadLine(), out int ctype);
                if (ctype == 1)
                {
                    WriteLine(practice[change] + "\n");
                    practice.RemoveAt(change);
                    entries = 0;
                    start = 10;
                    string l = string.Join("\n", practice.ToArray());
                    WriteLine("\n");
                    WriteLine(l);
                    WriteLine("GoodBye!");
                    ReadKey();
                }
                else if (ctype == 2)
                {
                    WriteLine(practice[change] + "\n");
                    WriteLine("Change to?");
                    int.TryParse(ReadLine(), out int u_change);
                    practice.Insert(change, u_change);
                    int del_change = change + 1;
                    practice.RemoveAt(del_change);
                    entries = 0;
                    start = 10;
                    string l = string.Join("\n", practice.ToArray());
                    WriteLine("\n");
                    WriteLine(l);
                    WriteLine("GoodBye!");
                    ReadKey();
                }
                else if (ctype == 3)
                {
                    WriteLine(practice[change] + "\n");
                    WriteLine("Enter addition to list.");
                    int.TryParse(ReadLine(), out int u_change);
                    practice.Insert(change + 1, u_change);
                    entries = 0;
                    start = 10;
                    string l = string.Join("\n", practice.ToArray());
                    WriteLine("\n");
                    WriteLine(l);
                    WriteLine("GoodBye!");
                    ReadKey();
                }
                else
                {
                    WriteLine("GoodBye!");
                    ReadKey();
                }
            }
            else
            {
                WriteLine("GoodBye!");
                ReadKey();
            }
        }
    }
}