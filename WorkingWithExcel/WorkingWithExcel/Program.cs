using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

using OfficeOpenXml;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
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
                    if (item.Contains("Qradar Tracking") || item.Contains("Remedy INC Info"))
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
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Worksheet1");                             
                var headerRow = new List<string[]>()
                {
                    new string[] { "Offense Type", "Total Notes", "Percent of Notes","Number tagged as Closed" }
                };
                var cellData = new List<object[]>()
                {
                };
                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                totalfiles = 0;
                foreach (string item in Notes)
                {
                    if (item.Contains("Qradar Tracking") || item.Contains("Remedy INC Info"))
                    {
                        item.Skip(1);
                    }
                    else
                    {
                        object[] files = Directory.GetFiles(item);
                        foreach (string offense in files)
                        {
                            if (offense.Contains("Format"))
                            {
                                offense.Skip(1);
                            }
                            else
                            {
                                totalfiles++;
                            }
                        }
                    }
                }
                foreach (string item in Notes)
                {
                    if (item.Contains("Qradar Tracking") || item.Contains("Remedy INC Info"))
                    {
                        item.Skip(1);
                    }
                    else
                    {
                        double typefiles = 0;
                        double taggedfiles = 0;
                        object name = Path.GetFileNameWithoutExtension(item);
                        object[] files = Directory.GetFiles(item);
                        foreach (string offense in files)
                        {
                            if (offense.Contains("Format"))
                            {
                                offense.Skip(1);
                            }
                            else if (offense.Contains("closed succesfully".ToLower()))
                            {
                                taggedfiles++;
                                typefiles++;
                            }
                            else
                            {
                                typefiles++;
                            }
                        }
                        cellData.Add(new object[] { name, typefiles, (typefiles / totalfiles).ToString("P"),taggedfiles});
                    }
                }
                var excelWorksheet1 = excel.Workbook.Worksheets["Worksheet1"];
                excelWorksheet1.Cells[headerRange].Style.Font.Bold = true;
                excelWorksheet1.Cells[headerRange].Style.Font.Size = 14;
                excelWorksheet1.Cells[headerRange].LoadFromArrays(headerRow);
                excelWorksheet1.Cells[2, 1].LoadFromArrays(cellData);

                var myChart = excelWorksheet1.Drawings.AddChart("chart", OfficeOpenXml.Drawing.Chart.eChartType.Pie);
                var chart2 = excelWorksheet1.Drawings.AddChart("chart2", OfficeOpenXml.Drawing.Chart.eChartType.Pie);

                var series = myChart.Series.Add("B2:B100", "A2: A100");
                var series2 = chart2.Series.Add("D2:D100", "A2: A100");

                myChart.Border.Fill.Color = System.Drawing.Color.Green;
                myChart.Title.Text = "Break Down of Current Types";
                myChart.SetSize(400, 400);
                myChart.SetPosition(1, 0, 6, 0);

                chart2.Border.Fill.Color = System.Drawing.Color.Green;
                chart2.Title.Text = "Closed Offenses";
                chart2.SetSize(400, 400);
                chart2.SetPosition(1, 0, 15, 25);

                FileInfo excelFile = new FileInfo(@"C:\Users\HarperD7\Documents\H.T I.R Aide\Notes\testbook.xlsx");
                excel.SaveAs(excelFile);
            }
            File.WriteAllLines(username + @"\Documents\H.T I.R Aide\Notes\testing.txt", doc);           
            
            
        }
        
    }
}