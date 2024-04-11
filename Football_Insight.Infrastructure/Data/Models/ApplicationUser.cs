using static Football_Insight.Infrastructure.Constants.DataConstants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Infrastructure.Data.Models
{
    [Comment("Represents the user profile within the application, extending the IdentityUser with custom properties for a personalized experience.")]
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(PersonFirstNameMaxLength)]
        [Comment("Optional. The user's first name. This is not required at registration and can be filled in later.")]
        public string? FirstName { get; set; } = string.Empty;

        [MaxLength(PersonLastNameMaxLength)]
        [Comment("Optional. The user's last name. This is not required at registration and can be filled in later.")]
        public string? LastName { get; set; } = string.Empty;

        [Comment("Optional. ID of the user's favorite team. This is not required at registration and can be filled in later.")]
        public int? FavoriteTeamId { get; set; }

        [Comment("Optional. ID of the user's favorite player. This is not required at registration and can be filled in later.")]
        public int? FavoritePlayerId { get; set; }

        [Comment("Optional. The user's country. This is not required at registration and can be filled in later.")]
        public string? Country {  get; set; } = string.Empty;

        [Comment("Optional. The user's city. This is not required at registration and can be filled in later.")]
        public string? City { get; set; } = string.Empty;

        [Comment("Optional. Path to the user's photo. This can be updated to enhance the user's profile.")]
        public string? PhotoPath { get; set; } = string.Empty;

        //A collection of the user's favorite matches. It is initialized to an empty list to avoid null reference exceptions.
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
