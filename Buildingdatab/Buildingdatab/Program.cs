using System;
using static System.Console;
using System.IO;
using ADOX;

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
            table.Name = "Table1";
            table.Columns.Append("Field1");
            table.Columns.Append("Field2");


            cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;" +
               "Data Source="+username+ @"\Documents\H.T I.R Aide\Notes\NewMDB.accdb; " +
               "Jet OLEDB:Engine Type=5");
            cat.Tables.Append(table);


            WriteLine("Database Created Successfully");
            ReadKey();

            cat = null;

        }
    }
}