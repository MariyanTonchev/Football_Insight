using Football_Insight.Data.Models;
using Football_Insight.Models.Player;
using Football_Insight.Models.Team;
using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Models.Account
{
    public class UserEditViewModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? FavoriteTeamId { get; set; }

        public int? FavoritePlayerId { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public IFormFile? Photo { get; set; }

        public string? PhotoPath { get; set; }

        public List<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();

        public List<PlayerDropdownViewModel> Players { get; set; } = new List<PlayerDropdownViewModel>();
    }
}
