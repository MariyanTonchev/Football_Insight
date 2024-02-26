using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Data.Models
{
    public class TeamStatistic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TeamId { get; set; }

        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Points { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; } = null!;
    }
}
