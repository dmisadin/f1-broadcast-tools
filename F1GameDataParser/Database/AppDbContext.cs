﻿using F1GameDataParser.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace F1GameDataParser.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        { 
        }

        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "F1BroadcastTools");
            Directory.CreateDirectory(folderPath);
            string dbPath = Path.Combine(folderPath, "f1BroadcastTools.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
