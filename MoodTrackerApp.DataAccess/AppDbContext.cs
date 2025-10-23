using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoodTrackerApp.Entities;

namespace MoodTrackerApp.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<MoodEntry> MoodEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=moodtracker.db");
        }
    }
}
