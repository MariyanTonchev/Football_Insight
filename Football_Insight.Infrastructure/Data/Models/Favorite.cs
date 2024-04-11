using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Infrastructure.Data.Models
{
    [Comment("Represent a relationship between a user and their favorite matches.")]
    public class Favorite
    {
        [Required]
        [Comment("The ID of the user who has marked a match as a favorite.")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [Comment("The ID of the match that has been marked as favorite by the user.")]
        public int MatchId { get; set; }

        //Navigation property for the ApplicationUser entity.
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        //Navigation property for the Match entity.
        [ForeignKey(nameof(MatchId))]
        public virtual Match Match { get; set; } = null!;
    }
}
