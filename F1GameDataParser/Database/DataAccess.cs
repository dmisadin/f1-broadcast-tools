using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace F1GameDataParser.Database
{
    public static class DataAccess
    {
        public static async Task InitializeDatabase()
        {
            // Get a suitable path for the database
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyApp");
            Directory.CreateDirectory(folderPath); // Ensure the folder exists
            string dbpath = Path.Combine(folderPath, "sqliteSample.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string tableCommand = "CREATE TABLE IF NOT EXISTS MyTable (" +
                                      "Primary_Key INTEGER PRIMARY KEY, " +
                                      "Text_Entry NVARCHAR(2048) NULL)";

                using (var createTable = new SqliteCommand(tableCommand, db))
                {
                    createTable.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Database initialized at: " + dbpath);
        }
    }
}
