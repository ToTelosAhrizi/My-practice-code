using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Douglas_School
{
    class Program
    {
        static void Main(string[] args)
        {
            
            

                School a = new School();





            WriteLine(a.AddSchool());
            ReadKey();
            WriteLine(a.ShowSchoolInfo());
            ReadKey();
            WriteLine(a.AddSchool());
            ReadKey();
            
                
                
            }
        }
    }
    class School
    {

    int i = 0;

    public string AddSchool()
        {
            string x = ("School has been added (Press enter to continue)\n");
            return x;
        }

    public string ShowSchoolInfo()
        {
            for (; ; ) {
            i++;
            WriteLine("Enter school name");
            string school_name = ReadLine();
            WriteLine("Eneter school address");
            string school_address = ReadLine();
            WriteLine("Eneter school phone number");
            string school_number = ReadLine();
            WriteLine("Eneter school student count");
            string school_count = ReadLine();
            
            string[] school_infotxt = {"School info below.\n\n" ,
                "School Name:                      " + school_name ,
                "\nSchool address:                   " + school_address ,
                "\nSchool phone number:              " + school_number ,
                "\nNumber of students in school:     " + school_count,
                "\n\n" };
            System.IO.File.WriteAllLines(@"C:\Users\HarperD7\Desktop\school_info" + i + ".txt", school_infotxt);

            string[] school_infocsv = {"School info below.\n\n" ,
                "School Name:," + school_name ,
                "School address:," + school_address ,
                "School phone number:," + school_number ,
                "Number of students in school:," + school_count,
                "\n\n" };
            System.IO.File.WriteAllLines(@"C:\Users\HarperD7\Desktop\school_info" + i + ".csv", school_infocsv);

            string school_infop = ("School info below.\n\n" +
                "School Name:                      " + school_name +
                "\nSchool address:                   " + school_address +
                "\nSchool phone number:              " + school_number +
                "\nNumber of students in school:     " + school_count +
                "\n\n(Press Enter to continue)\n" + i);

            WriteLine( school_infop );
        }
        }
        }

    
