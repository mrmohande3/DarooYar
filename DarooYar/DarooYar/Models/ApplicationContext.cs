using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace DarooYar.Models
{
    public class ApplicationContext :DbContext
    {
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<BloodTest> BloodTests { get; set; }

        public ApplicationContext()
        {
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "medicine.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
