using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodTrackerApp.Entities;

namespace MoodTrackerApp.DataAccess.Repositories
{
    public class MoodEntryRepository
    {
        public IEnumerable<MoodEntry> GetAll()
        {
            using var db = new AppDbContext();
            return db.MoodEntries.OrderByDescending(x => x.Date).ToList();
        }

        public MoodEntry GetByDate(DateTime date)
        {
            using var db = new AppDbContext();
            return db.MoodEntries.FirstOrDefault(x => x.Date.Date == date.Date);
        }

        public void Upsert(MoodEntry entry)
        {
            using var db = new AppDbContext();
            var existing = db.MoodEntries.FirstOrDefault(x => x.Date.Date == entry.Date.Date);

            if (existing == null)
                db.MoodEntries.Add(entry);
            else
            {
                existing.MoodScore = entry.MoodScore;
                existing.EnergyScore = entry.EnergyScore;
                existing.Tag = entry.Tag;
                existing.Note = entry.Note;
            }

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new AppDbContext();
            var entity = db.MoodEntries.Find(id);
            if (entity != null)
            {
                db.MoodEntries.Remove(entity);
                db.SaveChanges();
            }
        }
    }
}
