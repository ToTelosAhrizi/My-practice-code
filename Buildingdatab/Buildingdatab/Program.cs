using System;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Linq;
using static System.Console;
using System.Collections.Generic;

namespace Buildingdatab
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel.Application xlApp = new
            Microsoft.Office.Interop.Excel.Application();
            
            WriteLine("This report shows all relevant offenses (ignores the non offense folders, and non offense notes in offense folders).\n" +
                "It also gives just a couple of stats on the offense types.\n\n");
            double totalfiles = 0;
            double filetype = 0;
            string username = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            List<string> doc = new List<string>();
            List<string> Notes = new List<string>(Directory.GetDirectories(username + @"\Documents\H.T I.R Aide\Notes\"));
            string loop = "yes";
            while (loop.Equals("yes"))
            {
                foreach (string item in Notes)
                {
                    filetype = 0;
                    if (item.Contains("Qradar Tracking") || item.Contains("Remedy INC Info"))
                    {
                        item.Skip(1);
                    }
                    else
                    {
                        WriteLine(Path.GetFileNameWithoutExtension(item));
                        string[] files = Directory.GetFiles(item);
                        doc.Add(Path.GetFileNameWithoutExtension(item));
                        foreach (string file in files)
                        {
                            if (file.Contains("Format"))
                            {
                                file.Skip(1);
                            }
                            else
                            {
                                string checkclosed = File.ReadAllText(file);
                                if (checkclosed.ToLower().Contains("closed succesfully"))
                                {
                                    string newname = (Path.GetFullPath(file).Replace(".txt", ""));
                                    string newfile = newname + " tagged done";
                                    WriteLine("   " + Path.GetFileNameWithoutExtension(newfile));
                                    doc.Add("   " + Path.GetFileNameWithoutExtension(newfile));
                                    totalfiles++;
                                    filetype++;
                                }
                                else
                                {
                                    WriteLine("   " + Path.GetFileNameWithoutExtension(file));
                                    doc.Add("   " + Path.GetFileNameWithoutExtension(file));
                                    totalfiles++;
                                    filetype++;
                                }
                            }
                        }
                        WriteLine("Total: " + filetype);
                        doc.Add("Total: " + filetype);
                    }
                    
                }
                WriteLine("Grand Total: " + totalfiles);
                doc.Add("Grand Total: " + totalfiles);                
                filetype = 0;
                WriteLine("\nThis report shows all relevant offenses (ignores the non offense folders, and non offense notes in offense folders).\n" +
                "It also gives just a couple of stats on the offense types.\n");
                foreach (string item in Notes)
                {
                    if (item.Contains("Qradar Tracking")|| item.Contains("Remedy INC Info"))
                    {
                        item.Skip(1);
                    }
                    else
                    {
                        filetype = 0;
                        string[] files = Directory.GetFiles(item);
                        foreach (string file in files)
                        {
                            if (file.Contains("Format") || file.Contains("Closed Offenses"))
                            {
                                file.Skip(1);
                            }
                            else
                            {
                                string checkclosed = File.ReadAllText(file);
                                if (checkclosed.ToLower().Contains("closed succesfully") && !file.Contains("tagged done"))
                                {
                                    string newname = (Path.GetFullPath(file).Replace(".txt", ""));
                                    string newfile = newname + " tagged done";
                                }
                                filetype++;
                            }
                        }
                    }                    
                    double per = (filetype / totalfiles);
                    if (item.Contains("Qradar Tracking") || item.Contains("Remedy INC Info"))
                    {
                        item.Skip(1);
                    }
                    else
                    {
                        WriteLine(Path.GetFileNameWithoutExtension(item) + " percentage is " + per.ToString("P"));
                        doc.Add(Path.GetFileNameWithoutExtension(item) + " percentage is " + per.ToString("P"));
                        
                    }
                    
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
                            string[] hey = (File.ReadAllLines(file));
                            List<string> hi = new List<string>();
                            foreach (string line in hey)
                            {
                                hi.Add(line);
                            }
                            hi.Add("closed succesfully");
                            File.WriteAllLines(file, hi);                            
                        }                        
                    }
                }
                WriteLine("Again? (yes/no)");
                loop = ReadLine();
            }
            
            File.WriteAllLines(username + @"\Documents\H.T I.R Aide\Notes\testing.txt", doc);
            

        }
    }
}