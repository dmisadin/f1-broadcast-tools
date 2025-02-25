using Microsoft.EntityFrameworkCore;

namespace F1GameDataParser.Database
{
    public static class DataAccess
    {
        public static async Task InitializeAndMigrateDatabase()
        {
            using var db = new AppDbContext();
            await db.Database.MigrateAsync();
        }
    }
}
