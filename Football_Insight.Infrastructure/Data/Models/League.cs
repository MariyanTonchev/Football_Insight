using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Infrastructure.Data.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Team> Teams { get; set; } = new List<Team>();

        public ICollection<Match> Match { get; set; } = new List<Match>();
    }
}