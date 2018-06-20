using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace schedulemaker
{
    class Program
    {
        static void Main(string[] args)
        {
            int jan = 31;
            int feb = 28;
            int apr = 30;
            int mcho;
            int fullWeek;
            int wkDays;
            int hpw;
            int mon=0;int tue=0; int wed=0; int thu=0; int fri=0;
            
            WriteLine("enter the month you want to schedule");
            string monthChoice = ReadLine();
            if (monthChoice.ToLower().Contains("jan")|| monthChoice.ToLower().Contains("mar")|| monthChoice.ToLower().Contains("may") || monthChoice.ToLower().Contains("jul")||monthChoice.ToLower().Contains("au") || monthChoice.ToLower().Contains("oct")|| monthChoice.ToLower().Contains("de"))
            {
                WriteLine("that month has 31 days.");
                mcho = jan;
                fullWeek = mcho / 7;                
                WriteLine("that month has " + fullWeek + " full weeks.");                
                WriteLine("Monthly Max Hours?");
                int.TryParse(ReadLine(), out int mh);
                WriteLine("number of days to work per week desired");
                int.TryParse(ReadLine(), out int dpw);
                hpw = mh / fullWeek;
                if (hpw>30)
                {
                    int take = hpw - 30;
                    hpw -= take;
                }
                WriteLine(hpw);
                ReadKey();
                int[] week = new int[] { mon, tue, wed, thu, fri };
                for(int i =0; i<week.Count(); i++)
                {
                    if (week[i]<9)
                    {
                        week[i]++;
                    }
                }
                foreach (var item in week)
                {
                    WriteLine(item);
                }
                
            }
            else if (monthChoice.ToLower().Contains("ap") || monthChoice.ToLower().Contains("jun") || monthChoice.ToLower().Contains("se") || monthChoice.ToLower().Contains("no"))
            {
                WriteLine("that month has 30 days.");
                mcho = apr;
                fullWeek = mcho / 7;
                wkDays = mcho - (fullWeek * 2)-1;
                WriteLine("that month has " + fullWeek + " full weeks.");
                WriteLine("that month has " + wkDays + " work days.");
                WriteLine("Monthly Max Hours?");
                int.TryParse(ReadLine(), out int mh);
                WriteLine("number of days to work per week desired");
                int.TryParse(ReadLine(), out int dpw);
                


            }
            ReadKey();
        }
    }
}
