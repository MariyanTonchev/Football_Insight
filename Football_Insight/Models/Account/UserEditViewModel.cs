using Football_Insight.Data.Models;
using Football_Insight.Models.Player;
using Football_Insight.Models.Team;
using Microsoft.AspNetCore.Mvc;
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

        [FromForm]
        public IFormFile? Photo { get; set; }

        public string? PhotoPath { get; set; }

        public List<TeamSimpleViewModel> Teams { get; set; } = new List<TeamSimpleViewModel>();

        public List<PlayerSimpleViewModel> Players { get; set; } = new List<PlayerSimpleViewModel>();
    }
}
