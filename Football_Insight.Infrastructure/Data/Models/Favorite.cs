using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Infrastructure.Data.Models
{
    public class Favorite
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int MatchId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = null!;
    }
}
