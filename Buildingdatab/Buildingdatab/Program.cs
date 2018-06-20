using System;
using static System.Console;
using System.IO;
using ADOX;
using System.Data;
using System.Collections;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string username = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (File.Exists(username + @"\Documents\H.T I.R Aide\Notes\NewMDB.accdb"))
            {
                File.Delete(username + @"\Documents\H.T I.R Aide\Notes\NewMDB.accdb");
            }
            ADOX.Catalog cat = new ADOX.Catalog();            
            ADOX.Table table = new ADOX.Table();
           
            table.Name = "Overview";
            table.Columns.Append("Offense Type");
            table.Columns.Append("Total Notes");
            table.Columns.Append("Percent of Notes");
            table.Columns.Append("Number tagged as Closed");
            
            DataTable test = new DataTable();
            test.NewRow();
            DataColumn column;
            DataRow row;
            

            column = new DataColumn();
            column.ColumnName = "id";
            test.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "hi";
            test.Columns.Add(column);
            ArrayList hi = new ArrayList();
            for (int i = 0; i < 10; i++)
            {
                row = test.NewRow();
                row["id"] = i;
                row["hi"] = i+1;
                test.Rows.Add(row);
                hi.Add(row["id"] + "  " + row["hi"]);
                WriteLine(row["id"]+"  "+row["hi"]);
            }
            string[] nope= new string [hi.Count] ;
            hi.CopyTo(nope);
            File.WriteAllLines(username + @"\Documents\H.T I.R Aide\Notes\testNewMDB.accdb",nope);

            cat.Create("Provider =Microsoft.Jet.OLEDB.4.0;" +
               "Data Source="+username+ @"\Documents\H.T I.R Aide\Notes\NewMDB.accdb; " +
               "Jet OLEDB:Engine Type=5");
            cat.Tables.Append(table);


            WriteLine("Database Created Successfully");
            ReadKey();

            cat = null;

        }
    }
}