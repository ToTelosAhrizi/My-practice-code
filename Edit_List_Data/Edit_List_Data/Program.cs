using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Changing_List_Data
{/// <summary>
/// This program will allow the user to input data into a list. Once input they will be allowed to edit the list by deleting one item, change the value of one
/// item, or adding one item to a specific point in the list.
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<int> practice = new List<int>();//first a new list is created and named practice
            practice.Add(1);//list start at zero, so as to not to confuse the user the start point of the list is set to zero
            int start = 1;//All list must have at least one entry so we are setting this
            int x = 2;//x will be the variable being added to the list. It gets its first value here
            WriteLine("How many entries?(Enter a valid (whole, positive) number. Non Valid entries will close.)");
            if (int.TryParse(ReadLine(), out int entries)&&entries>0)// this tells the progam what to do if the user enters a valid number of how many items to add to the list.
            {
                /*will add x to the list, raise the value of x and raise the value of start. Will loop this action 
                 * until start is equal to the number of entries the user input*/
                while (start != entries)
                {
                    practice.Add(x);
                    x++;
                    start++;
                }
                
                string p = string.Join("\n", practice.ToArray());//adds list to an array and converts the list to a human readable string for display
                WriteLine(p);//displaying the values of the practice array
                WriteLine("\nWhat entry would you like to change");//ask what item in the list they want to change
                if (int.TryParse(ReadLine(), out int change)&& practice.Contains(change))//reads what item in the list the user wants to change
                {
                    WriteLine("\nPress 1 to delete, 2 to update, 3 to add an entry. All else will close.");//ask the user what they want to do to that item in the list
                    int.TryParse(ReadLine(), out int ctype);//reads what the user wants to do to the selected item in the list
                    if (ctype == 1)//what to do to the user selected item in the list if they chose delete the seleced item
                    {
                        WriteLine(practice[change - 1] + "\n");//shows what item was selected
                        practice.RemoveAt(change - 1);         // deletes selected item
                        string l = string.Join("\n", practice.ToArray());//creates a new array with updated info and converts it to a string for human readability
                        WriteLine("\n");
                        WriteLine(l);//displays the converted array
                        WriteLine("GoodBye!");
                        ReadKey();
                    }
                    else if (ctype == 2)//what to do to the user selected item in the list if they chose change the selected items value
                    {
                        WriteLine(practice[change - 1] + "\n");//shows what item was selected
                        WriteLine("Change to?");//Ask the user what they want value they want to change the selected item to
                        int.TryParse(ReadLine(), out int u_change);//Reads what to change the selected items value to
                        practice.Insert(change - 1, u_change);//Adds the user input item to the list
                        int del_change = change;//finds the new postion for the selected input
                        practice.RemoveAt(del_change);//removes the user selected value from the list                    
                        string l = string.Join("\n", practice.ToArray());//creates a new array with updated info and converts it to a string for human readability
                        WriteLine("\n");
                        WriteLine(l);//displays the converted array
                        WriteLine("GoodBye!");
                        ReadKey();
                    }
                    else if (ctype == 3)//what to do to the user selected item in the list if they chose to add an item to the list after the selected item.
                    {
                        WriteLine(practice[change - 1] + "\n");//shows what item was selected
                        WriteLine("Enter addition to list.");
                        int.TryParse(ReadLine(), out int u_change);//Reads what value the user wants to add to the list
                        practice.Insert(change, u_change);//find where in the list to add the users input and inputs it into the list
                        string l = string.Join("\n", practice.ToArray());//creates a new array with updated info and converts it to a string for human readability
                        WriteLine("\n");
                        WriteLine(l);//displays the converted array
                        WriteLine("GoodBye!");
                        ReadKey();
                    }
                    else//what to do if the user doesnt input a valid entry to change the list
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
            else// what to do if the user doesnt provide a valid entry for the number of entries in the list
            {
                WriteLine("GoodBye!");
                ReadKey();
            }
        }
    }
}