using Microsoft.AspNetCore.Identity;

namespace Football_Insight.Data.Models
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
    }
}
