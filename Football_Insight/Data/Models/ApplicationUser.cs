using Microsoft.AspNetCore.Identity;

namespace Football_Insight.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public string? FavoriteTeam { get; set; } = string.Empty;

        public string? FavoritePlayer { get; set; } = string.Empty;

        public string? Country {  get; set; } = string.Empty;

        public string? City { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string? PhotoPath { get; set; } = string.Empty;
    }
}
