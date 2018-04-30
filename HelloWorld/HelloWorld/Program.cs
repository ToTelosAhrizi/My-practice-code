using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //foreach(string s in Directory.GetDirectories(username))
            //{
            //    Console.WriteLine(s.Remove(0, username.Length));
            //}
            string un = username.Remove(0, 9);
            WriteLine("Hello " + username);
            WriteLine("Hello " + un);
            ReadKey();
        }
    }
}
