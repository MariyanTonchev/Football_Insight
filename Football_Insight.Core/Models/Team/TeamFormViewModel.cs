using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Stadium;
using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Core.Models.Team
{
    public class TeamFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Founded { get; set; }

        [Required]
        public string LogoURL { get; set; } = string.Empty;

        [Required]
        public string Coach { get; set; } = null!;

        [Required]
        public int LeagueId { get; set; }

        [Required]
        public int StadiumId { get; set; }

        public List<StadiumSimpleViewModel> Stadiums { get; set; } = new List<StadiumSimpleViewModel>();

        public List<LeagueSimpleViewModel> Leagues { get; set; } = new List<LeagueSimpleViewModel>();
    }
}
