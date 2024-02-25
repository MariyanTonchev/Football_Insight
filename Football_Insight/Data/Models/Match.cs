using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Data.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int StadiumId { get; set; }

        [Required]
        public int HomeScore { get; set; }

        [Required]
        public int AwayScore { get; set; }

        [ForeignKey(nameof(HomeTeamId))]
        public Team HomeTeam { get; set; } = null!;

        [ForeignKey(nameof(AwayTeamId))]
        public Team AwayTeam { get; set; } = null!;

        [ForeignKey(nameof(StadiumId))]
        public Stadium Venue { get; set; } = null!;

        public ICollection<PlayerMatch> PlayersMatches { get; set; } = new List<PlayerMatch>();
    }
}