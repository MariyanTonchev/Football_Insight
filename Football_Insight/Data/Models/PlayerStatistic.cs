using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Data.Models
{
    public class PlayerStatistic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public int MatchId { get; set; }

        public int? Goals { get; set; }

        public int? Assists { get; set; }

        public int? MinutesPlayed { get; set; }

        public int? YellowCards { get; set; } 

        public int? RedCards { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; } = null!;

        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = null!;
    }
}