using Microsoft.Data.Sqlite;

namespace F1GameDataParser.Database
{
    public static class DataAccess
    {
        public static async Task InitializeDatabase()
        {
            // Get a suitable path for the database
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "F1BroadcastTools");
            Directory.CreateDirectory(folderPath); // Ensure the folder exists
            string dbpath = Path.Combine(folderPath, "f1BroadcastTools.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string tableCommand = "CREATE TABLE IF NOT EXISTS Players (" +
                                      "Id INTEGER PRIMARY KEY, " +
                                      "Name NVARCHAR(2048) NOT NULL, " +
                                      "Nationality INTEGER NULL)";

                using (var createTable = new SqliteCommand(tableCommand, db))
                {
                    createTable.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Database initialized at: " + dbpath);
        }
    }
}
