using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Stadium;
using System.ComponentModel.DataAnnotations;
using static Football_Insight.Infrastructure.Constants.DataConstants;
using static Football_Insight.Core.Constants.ValidationMessages;

namespace Football_Insight.Core.Models.Team
{
    public class TeamFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(YearFoundMin, YearFoundMax, ErrorMessage = StringRangeErrorMessage)]
        [Display(Name = "Year Founded")]
        public int Founded { get; set; }

        [Required]
        [Display(Name = "Logo URL")]
        public string LogoURL { get; set; } = string.Empty;

        [Required]
        [StringLength(CoachNameMaxLength, MinimumLength = CoachNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Coach { get; set; } = null!;

        [Required]
        [Display(Name = "League")]
        public int LeagueId { get; set; }

        [Required]
        [Display(Name = "Stadium")]
        public int StadiumId { get; set; }

        public List<StadiumSimpleViewModel> Stadiums { get; set; } = new List<StadiumSimpleViewModel>();

        public List<LeagueSimpleViewModel> Leagues { get; set; } = new List<LeagueSimpleViewModel>();
    }
}
