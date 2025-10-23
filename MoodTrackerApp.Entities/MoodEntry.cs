using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodTrackerApp.Entities
{
    [Table("MoodEntries")]
    public class MoodEntry
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Range(1, 10)]
        public int MoodScore { get; set; }

        [Range(1, 10)]
        public int EnergyScore { get; set; }

        [MaxLength(50)]
        public string Tag { get; set; }

        [MaxLength(1000)]
        public string Note { get; set; }
    }
}
