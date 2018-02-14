using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace ScratchPad2
{
    class Program
    {
        static void Main(string[] args)
        {
            string win_year= "0";
            string curFile = @"C:\Users\HarperD7\Desktop\info" + win_year + ".txt";
            WriteLine(File.Exists(curFile) ? "File exists." : "File does not exist.");
            ReadKey();
        }
       
    }
}
