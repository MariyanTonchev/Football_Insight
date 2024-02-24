using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Data.Models
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int YearBuilt { get; set; }

        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
