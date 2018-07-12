using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;


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
                                if (checkclosed.ToLower().Contains("y, closed successfully"))
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
                                if (checkclosed.ToLower().Contains("y, closed successfully") && !file.Contains("tagged done"))
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
                            IEnumerable<string> inc = hi.Where(i => i.Length == 15 && i.Contains("INC"));
                            if (inc.Contains("INC")&&inc.Count()==15)
                            {
                                hi.Add("y, closed successfully");
                            }
                            else
                            {
                                hi.Add("y, moved to level 2 or inactive");
                            }
                            
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
                excel.Workbook.Worksheets.Add("30 Days");
                excel.Workbook.Worksheets.Add("30 Days Detailed");
                var headerRow = new List<string[]>()
                {
                    new string[] { "Offense Type", "Total Notes", "Percent of Notes","Number tagged as Closed"}
                };

                var headerSheet2 = new List<string[]>()
                {
                    new string[] { "Offense Type", "Offense Number", "Remedy INC","Tag","Number of Events","Day Started","Last Edited","Process Time (In Hours)", "Process Time (In Days)" }
                };

                var cellData = new List<object[]>()
                {
                };

                var sheet2Data = new List<object[]>()
                {
                };

                var sheet3Data = new List<object[]>()
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
                        string createcheck;
                        DateTime editcheck;
                        foreach (string offense in files)
                        {
                            if (offense.Contains("Format"))
                            {
                                offense.Skip(1);
                            }                            
                            else
                            {
                                string datn = DateTime.Now.ToString();
                                int.TryParse(datn, out int dtn);
                                createcheck = File.GetCreationTime(offense).ToString();
                                int.TryParse(createcheck, out int ch);
                                editcheck = File.GetLastWriteTime(offense);
                                typefiles++;
                                string tagcheck = File.ReadAllText(offense);
                                if (tagcheck.Contains("y, closed successfully")|| tagcheck.Contains("y, moved to level 2"))
                                {
                                    taggedfiles++;
                                }
                                if (ch <= dtn + 15)
                                {
                                    sheet3Data.Add(new object[] { name, typefiles, (typefiles / totalfiles).ToString("P"), taggedfiles }); ;
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
                                IEnumerable<string> eventnum = checkclosed.Where(i => i.Length > 6 && i.Length < 25 && i.Contains("events"));
                                string evnn = string.Join("", eventnum.ToArray());
                                string evennum = string.Concat(evnn.Reverse().Skip(7).Reverse());
                                double.TryParse(evennum, out double numeven);                                
                                if (checkclosed.Contains("y, closed successfully")|| checkclosed.Contains("y, moved to level 2 or inactive"))
                                {
                                    string newname = (Path.GetFullPath(file).Replace(".txt", ""));
                                    string tag = "Closed";
                                    DateTime createdate = File.GetCreationTime(file);
                                    DateTime editdate = File.GetLastWriteTime(file);
                                    string bd = createdate.ToString("MM/dd/yy");
                                    string ed = editdate.ToString("MM/dd/yy");
                                    TimeSpan time = (editdate - createdate);
                                    int days;
                                    if (time.TotalHours > 24)
                                    {
                                        days = time.Days;
                                    }
                                    else
                                    {
                                        days = 1;
                                    }
                                    string t = time.TotalHours.ToString("##.##");
                                    sheet2Data.Add(new object[] { name, Path.GetFileNameWithoutExtension(newname), inc, tag, numeven,createdate, editdate, t,days });
                                    



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
                                    int days;
                                    if (time.TotalHours>24)
                                    {
                                        days = time.Days;
                                    }
                                    else
                                    {
                                        days = 1;
                                    }
                                    
                                    string t = (time.TotalHours.ToString("##.##")+" - Still Working");
                                    sheet2Data.Add(new object[] { name, Path.GetFileNameWithoutExtension(newname), inc, tag, numeven, createdate, editdate, t,days });


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
                int tagging = 1;
                var excelWorksheet2 = excel.Workbook.Worksheets["Details"];

                var excelWorksheet3 = excel.Workbook.Worksheets["30 Days"];
                excelWorksheet3.Cells[headerRange].Style.Font.Bold = true;
                excelWorksheet3.Cells[headerRange].Style.Font.Size = 14;
                excelWorksheet3.Cells[headerRange].LoadFromArrays(headerRow);
                excelWorksheet3.Cells[2, 1].LoadFromArrays(cellData);

                var excelWorksheet4 = excel.Workbook.Worksheets["30 Days Detailed"];

                foreach (var item in sheet2Data)
                {
                    tagging++;
                    if (item.Contains("Closed"))
                    {                         
                        string closeRange = "A" + tagging + ":" + "Z" +tagging;
                        excelWorksheet2.Cells[headerRange].Style.Font.Bold = true;
                        excelWorksheet2.Cells[headerRange].Style.Font.Size = 14;


                        excelWorksheet2.Cells[closeRange].Style.Font.Bold = true;
                        excelWorksheet2.Cells[closeRange].Style.Font.UnderLine = true;
                        excelWorksheet2.Cells[closeRange].Style.Font.Color.SetColor(System.Drawing.Color.LimeGreen);
                        excelWorksheet2.Cells[headerRange].LoadFromArrays(headerSheet2);
                        excelWorksheet2.Cells[2, 1].LoadFromArrays(sheet2Data);
                    }
                    else
                    {
                        string closeRange = "A" + tagging + ":" + "Z" + tagging;
                        excelWorksheet2.Cells[headerRange].Style.Font.Bold = true;
                        excelWorksheet2.Cells[headerRange].Style.Font.Size = 14;
                        excelWorksheet2.Cells[headerRange].LoadFromArrays(headerSheet2);
                        excelWorksheet2.Cells[2, 1].LoadFromArrays(sheet2Data);
                    }
                   
                }
                ExcelRange rng = excelWorksheet2.Cells[1,1,sheet2Data.Count()+1,9];
                ExcelTable table = excelWorksheet2.Tables.Add(rng,"table");

                ExcelRange rng1 = excelWorksheet1.Cells[1, 1, cellData.Count() + 1, 4];
                ExcelTable table1 = excelWorksheet1.Tables.Add(rng1, "main-table");

                table1.Columns[0].Name = "Offense Type";
                table1.Columns[1].Name = "Total Notes";
                table1.Columns[2].Name = "Percent of Notes";
                table1.Columns[3].Name = "Number tagged as Closed";

                table.Columns[0].Name = "Offense Type";
                table.Columns[1].Name = "Offense Number";
                table.Columns[2].Name = "Remedy INC";
                table.Columns[3].Name = "Tag";
                table.Columns[4].Name = "Number of Events";
                table.Columns[5].Name = "Day Started";
                table.Columns[6].Name = "Last Edited";
                table.Columns[7].Name = "Process Time (In Hours)";
                table.Columns[8].Name = "Process Time (In Days)";

                var myChart = excelWorksheet1.Drawings.AddChart("chart", OfficeOpenXml.Drawing.Chart.eChartType.Pie);
                var chart2 = excelWorksheet1.Drawings.AddChart("chart2", OfficeOpenXml.Drawing.Chart.eChartType.BarClustered);

                var series = myChart.Series.Add("B2:B" + tagged, "A2: A" + tagged);
                var series2 = chart2.Series.Add("D2:D" + tagged, "A2: A" + tagged);
                var series3 = chart2.Series.Add("B2:B" + tagged, "A2: A" + tagged);

                myChart.Border.Fill.Color = System.Drawing.Color.Green;
                myChart.Title.Text = "Break Down of Current Types";
                myChart.SetSize(1200, 650);
                chart2.SetPosition(1, 0, 6, 0);

               
                chart2.Border.Fill.Color = System.Drawing.Color.Green;
                chart2.SetSize(1000,500);
                chart2.Title.Text = "Closed Offenses vs Total Offenses";
                chart2.Series[1].Header = "Note Type Total";
                chart2.Series[0].Header = "Note Type Closed";

                 myChart.SetPosition((tagged*2-5), 0, 6, 0);
                chart2.Legend.Add();

                FileInfo excelFile = new FileInfo(username + @"\Documents\H.T I.R Aide\Metrics\Metrics run "+DateTime.Now.ToString("MM.dd.yy")+" at "+DateTime.Now.ToString("hh.mm") + ".xlsx");
                excel.SaveAs(excelFile);
                
               
                
            }

        }
    }
}