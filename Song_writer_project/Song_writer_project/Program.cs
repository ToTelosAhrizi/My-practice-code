using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Song_writer_project
{
    class Program
    {

        static void Main(string[] args)
        {
            for(; ; )
            {
                SongInformation a = new SongInformation();
                string choice;
                WriteLine("Would you like to enter a new song(1) or see a previous entry(2)");
                choice = ReadLine();
                if (int.TryParse(choice, out int choose))
                {
                    if (choose == 1)
                    {
                        Write(a.SongInfo());
                        ReadKey();
                    }
                    else if (choose == 2)
                    {
                        Write(a.ShowInfo());
                        ReadKey();
                    }
                    else
                    {
                        WriteLine("NonValid entry");
                        ReadKey();
                    }
                }
                else
                {
                    WriteLine("NonValid entry");
                    ReadKey();
                }
            }
                 
            
                
        }
    }
    class SongInformation
    {
        int lines;        
        int version;
        string line;
        string song;
        string ver;
        
        public string SongInfo()
        {
            WriteLine("Whats your songs name?");
            song = ReadLine();

            WriteLine("What version of the song is this?");
            ver = ReadLine();
            int.TryParse(ver, out version);

            WriteLine("How many lines?");
            line = ReadLine();
            int.TryParse(line, out lines);
            int startl = 1;
            List<string> lyric = new List<string>();
            WriteLine("Enter you lyrics, make sure you have enough lines to enter");
            while (startl <= lines)
            {
                Write(startl+". ");
                lyric.Add(ReadLine()); 
                startl++;                
            }
            string lyrics =string.Join("\n",lyric.ToArray());
            
            string[] infotxt = {"\nInformation listed below.\n\n" ,
                "Song Name:                      " + song,
                "Version:                        "+version,
                "The Lyrics:                     \n"+lyrics
            };

            System.IO.File.WriteAllLines(@"C:\Users\Jay\Desktop\Song_" + song +"_v_"+ version + ".txt", infotxt);
            string infop = ("Success! Press enter!");
            return infop;
        }
        public string ShowInfo()
        {
            
            WriteLine("What song do you want to see?");
            song = ReadLine();
            WriteLine("What version do you want to see?");
            ver = ReadLine();
            int.TryParse(ver, out version);
            string checking = @"C:\Users\Jay\Desktop\Song_" + song + "_v_" + version + ".txt";
            return (File.Exists(checking) ? System.IO.File.ReadAllText(@"C:\Users\Jay\Desktop\Song_" + song + "_v_" + version + ".txt")
                : "File does not exist. Press Enter to try again.");
        }
    }

}
