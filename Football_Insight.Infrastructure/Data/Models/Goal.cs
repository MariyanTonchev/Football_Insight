using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Insight.Infrastructure.Data.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Required]
        public int MatchId { get; set; }

        [Required]
        public int ScoringPlayerId { get; set; }

        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = null!;

        [ForeignKey(nameof(ScoringPlayerId))]
        public Player ScoringPlayer { get; set; } = null!;
    }
}
