using Microsoft.AspNetCore.Identity;

namespace Football_Insight.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public int? FavoriteTeamId { get; set; }

        public int? FavoritePlayerId { get; set; }

        public string? Country {  get; set; } = string.Empty;

        public string? City { get; set; } = string.Empty;

        public string? PhotoPath { get; set; } = string.Empty;

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
