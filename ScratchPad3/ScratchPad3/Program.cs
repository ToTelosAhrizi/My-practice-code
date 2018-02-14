using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace ScratchPad3
{
    class Program
    {
        static void Main(string[] args)
        {
            Info one = new Info(); //Creating a new object for the new class.
            for (; ; ) //Makes the program loop forever. Loop 1.
            {
                /*Asking for user input. The choice must be between 1 and 2. 
                 * If any other input is entered an error message will display.*/
                 
                WriteLine("Press 1 to enter info, press 2 to see most updated list.\n"); 
                string choice = ReadLine();               
                if (int.TryParse(choice, out int choose))//check to see if they entered a number
                {
                    if (choose == 1)//Choice for calling method GetInfo
                    {
                        Write(one.GetInfo());
                        ReadKey();
                    }
                    else if (choose == 2)// Choice for calling method ShowInfo
                    {
                        Write(one.ShowInfo());
                        ReadKey();
                    }
                    else//error if they put in incorrect number
                    {
                        WriteLine("Non valid entry. Enterd non valid number. Press enter to try again.\n");
                        ReadKey();
                    }
                }
                else// error for entered non number
                {
                    WriteLine("Non valid entry. Entered a non numerical. Press enter to try again.\n");
                    ReadKey();
                }
            }
        }
    }
    /*Next the Info class is created.
     Inside of this is the methods GetInfo and ShowInfo are created.*/

    class Info// creating class info
    {
        public string GetInfo()
        {
            string year;
            long phone_num = 0;
            int win_year = 0;
            bool phone_loop = true;
            bool year_loop = true;
            WriteLine("Enter winners name");
            string name = ReadLine();
            WriteLine("Enter winners address");
            string address = ReadLine();
            while (phone_loop)
            {
                WriteLine("Enter winners phone number");
                string number = ReadLine();
                if (long.TryParse(number, out phone_num) && number.Length == 10)
                {
                    phone_loop = false;
                }
                else
                {
                    WriteLine("Non valid enrty. Try again.");
                    phone_loop = true;
                }
            }
            while (year_loop)
            {
                WriteLine("Enter the year they won number");
                year = ReadLine();

                if (int.TryParse(year, out win_year) && year.Length == 4)
                {
                    year_loop = false;
                }
                else
                {
                    WriteLine("Non valid enrty. Try again.");
                    year_loop = true;
                }
            }
            string[] infotxt = {"\nInformation listed below.\n\n" ,
                "Name:                      " + name ,
                "\nAddress:                   " + address ,
                "\nPhone number:              " + phone_num ,
                "\nThe year they won:            "+win_year,
                "\n\n" };
            System.IO.File.WriteAllLines(@"C:\Users\HarperD7\Desktop\info" + win_year + ".txt", infotxt);
            string infop = ("Success! Press enter!");
            return infop;                 
        }
        public string ShowInfo()
        {
            int win_year;
            WriteLine("What year do you want to see? (YYYY)");
            string year = ReadLine();
            int.TryParse(year, out win_year);
            string checking = @"C:\Users\HarperD7\Desktop\info" + win_year + ".txt";
            return (File.Exists(checking) ? System.IO.File.ReadAllText(@"C:\Users\HarperD7\Desktop\info" + win_year + ".txt")
                : "File does not exist. Press Enter to try again.");
        }
    }
}
