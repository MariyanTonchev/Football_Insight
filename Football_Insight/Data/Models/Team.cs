using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Data.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Founded { get; set; }

        [Required]
        public int LeagueId { get; set; }

        [Required]
        public int VenueId { get; set; }

        [ForeignKey(nameof(LeagueId))]
        public League League { get; set; } = null!;

        public Coach Coach { get; set; } = null!;

        [ForeignKey(nameof(VenueId))]
        public Venue Venue { get; set; } = null!;

        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    }
}