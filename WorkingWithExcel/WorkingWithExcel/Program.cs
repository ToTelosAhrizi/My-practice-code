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
                                if (checkclosed.ToLower().Contains("closed successfully"))
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
                                if (checkclosed.ToLower().Contains("closed successfully") && !file.Contains("tagged done"))
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
                            hi.Add("closed successfully");
                            File.WriteAllLines(file, hi);
                        }
                    }
                }
                WriteLine("Again? (yes/no)");
                loop = ReadLine();
            }
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Overview");
                excel.Workbook.Worksheets.Add("Details");
                var headerRow = new List<string[]>()
                {
                    new string[] { "Offense Type", "Total Notes", "Percent of Notes","Number tagged as Closed"}
                };

                var headerSheet2 = new List<string[]>()
                {
                    new string[] { "Offense Type", "Offense Number", "Remedy INC","Tag","Day Started","Last Edited","Process Time (In Hours)"  }
                };

                var cellData = new List<object[]>()
                {
                };

                var sheet2Data = new List<object[]>()
                {
                };

                string headerRange = "A1:Z1";
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
                int tagged = 1;
                
                foreach (string item in Notes)
                {
                    
                    if (item.Contains("Qradar Tracking") || item.Contains("Remedy INC Info"))
                    {
                        item.Skip(1);
                    }
                    else
                    {
                        tagged++;
                        double typefiles = 0;
                        int taggedfiles = 0;
                        object name = Path.GetFileNameWithoutExtension(item);
                        object[] files = Directory.GetFiles(item);
                        
                        foreach (string offense in files)
                        {
                            if (offense.Contains("Format"))
                            {
                                offense.Skip(1);
                            }                            
                            else
                            {                                
                                typefiles++;
                                string tagcheck = File.ReadAllText(offense);
                                if (tagcheck.Contains("closed successfully"))
                                {
                                    taggedfiles++;
                                }
                            }
                        }
                        cellData.Add(new object[] { name, typefiles, (typefiles / totalfiles).ToString("P"), taggedfiles });
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
                        
                        
                        object name = Path.GetFileNameWithoutExtension(item);
                        object[] files = Directory.GetFiles(item);
                        foreach (string file in files)
                        {
                            if (file.Contains("Format"))
                            {
                                file.Skip(1);
                            }
                            else
                            {
                                
                                List<string> checkclosed = new List<string>(File.ReadAllLines(file));                                
                                IEnumerable<string> inc = checkclosed.Where(i=>i.Length==15&&i.Contains("INC"));                               
                                if (checkclosed.Contains("y, closed successfully"))
                                {
                                    string newname = (Path.GetFullPath(file).Replace(".txt", ""));
                                    string tag = "Closed";
                                    DateTime createdate = File.GetCreationTime(file);
                                    DateTime editdate = File.GetLastWriteTime(file);
                                    string bd = createdate.ToString("MM/dd/yy");
                                    string ed = editdate.ToString("MM/dd/yy");
                                    TimeSpan time = (editdate - createdate);
                                    string t = time.TotalHours.ToString("##.##");
                                    sheet2Data.Add(new object[] { name, Path.GetFileNameWithoutExtension(newname), inc, tag, bd, ed, t });
                                    



                                }
                                else
                                {
                                    string newname = (Path.GetFullPath(file).Replace(".txt", ""));
                                    string tag = "Open";
                                    DateTime createdate = File.GetCreationTime(file);
                                    DateTime editdate = File.GetLastWriteTime(file);
                                    string bd = createdate.ToString("MM/dd/yy");
                                    string ed = editdate.ToString("MM/dd/yy");
                                    TimeSpan time = (editdate - createdate);
                                    string t = "Still Working";
                                    sheet2Data.Add(new object[] { name, Path.GetFileNameWithoutExtension(newname), inc, tag, bd, ed, t });


                                }
                            }                            

                        }
                        
                    }

                }
                var excelWorksheet1 = excel.Workbook.Worksheets["Overview"];
                excelWorksheet1.Cells[headerRange].Style.Font.Bold = true;
                excelWorksheet1.Cells[headerRange].Style.Font.Size = 14;
                excelWorksheet1.Cells[headerRange].LoadFromArrays(headerRow);
                excelWorksheet1.Cells[2, 1].LoadFromArrays(cellData);

                var excelWorksheet2 = excel.Workbook.Worksheets["Details"];
                excelWorksheet2.Cells[headerRange].Style.Font.Bold = true;
                excelWorksheet2.Cells[headerRange].Style.Font.Size = 14;
                excelWorksheet2.Cells[headerRange].LoadFromArrays(headerSheet2);
                excelWorksheet2.Cells[2, 1].LoadFromArrays(sheet2Data);

                var myChart = excelWorksheet1.Drawings.AddChart("chart", OfficeOpenXml.Drawing.Chart.eChartType.Pie);
                var chart2 = excelWorksheet1.Drawings.AddChart("chart2", OfficeOpenXml.Drawing.Chart.eChartType.ColumnStacked);

                var series = myChart.Series.Add("B2:B" + tagged, "A2: A" + tagged);
                var series2 = chart2.Series.Add("D2:D" + tagged, "A2: A" + tagged);

                myChart.Border.Fill.Color = System.Drawing.Color.Green;
                myChart.Title.Text = "Break Down of Current Types";
                myChart.SetSize(tagged * 22);
                myChart.SetPosition(1, 0, 6, 0);
                
                
                chart2.Border.Fill.Color = System.Drawing.Color.Green;
                chart2.SetSize(tagged*22);
                chart2.Title.Text = "Closed Offenses";
                chart2.Legend.Remove();
                chart2.SetPosition((tagged*3+1), 0, 6, 0);

                FileInfo excelFile = new FileInfo(username + @"\Documents\H.T I.R Aide\Notes\testbook.xlsx");
                excel.SaveAs(excelFile);
            }
        }
    }
}