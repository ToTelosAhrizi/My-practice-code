using System;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Buildingdatab
{
    class Program
    {
        static void Main(string[] args)
        {
            double totalfiles= 0;
            string username = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            List<string> doc = new List<string>();
            List<string> Notes = new List<string>(Directory.GetDirectories(username+ @"\Documents\H.T I.R Aide\Notes\"));
            foreach (string item in Notes)
            {
                WriteLine(Path.GetFileNameWithoutExtension(item));                
                double filetype = 0;
                string [] files = Directory.GetFiles(item);
                doc.Add(Path.GetFileNameWithoutExtension(item));
                foreach (string file in files)
                {
                    WriteLine("   "+Path.GetFileNameWithoutExtension(file));
                    doc.Add("   " + Path.GetFileNameWithoutExtension(file));
                    totalfiles++;
                    filetype++;
                }
                WriteLine("Total: " + filetype);
                doc.Add("Total: " + filetype);
            }
            WriteLine("Grand Total: " + totalfiles);
            doc.Add("Grand Total: " + totalfiles);
            foreach (string item in Notes)
            {
                double filetype = 0;
                string[] files = Directory.GetFiles(item);
                foreach (string file in files)
                {
                    filetype++;
                }
                double per = (filetype/totalfiles);
                WriteLine(Path.GetFileNameWithoutExtension(item) + " percentage is " + per.ToString("P"));
                doc.Add(Path.GetFileNameWithoutExtension(item) + " percentage is " + per.ToString("P"));
            }
            WriteLine("Type the ones you want to tag as closed. Separate by comma");
            string[] cho = ReadLine().Split(',');
            foreach (string item in Notes)
            {
                string[] files = Directory.GetFiles(item);
                foreach (string file in files)
                {
                   
                    if (cho.Contains(Path.GetFileNameWithoutExtension(file)))
                    {
                        string newname =(Path.GetFullPath(file).Replace(".txt", ""));
                        File.Copy(file,newname+" done.txt");
                        File.Delete(file);
                    }
                }

            }
            
            File.WriteAllLines(username + @"\Documents\H.T I.R Aide\Notes\testing.txt", doc);
            ReadKey();
        }
    }
}
