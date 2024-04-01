using Football_Insight.Core.Models.Team;
using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Core.Models.Match
{
    public class MatchFormViewModel : IValidatableObject
    {
        [Required]
        public int MatchId { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; } = DateTime.Today;

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int LeagueId { get; set; }

        public ICollection<TeamSimpleViewModel> Teams { get; set; } = new List<TeamSimpleViewModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateAndTime < DateTime.Now)
            {
                yield return new ValidationResult(
                        "The date and time should be in the future!",
                        new[] { nameof(DateAndTime) });
            }

            if (HomeTeamId == 0)
            {
                yield return new ValidationResult(
                        "The Home Team is required. Please make a selection!",
                        new[] { nameof(HomeTeamId) });
            }

            if (AwayTeamId == 0)
            {
                yield return new ValidationResult(
                        "The Away Team is required. Please make a selection!",
                        new[] { nameof(AwayTeamId) });
            }

            if (HomeTeamId == AwayTeamId)
            {
                yield return new ValidationResult(
                        "The Home Team and Away Team cannot be the same.",
                        new[] { nameof(HomeTeamId), nameof(AwayTeamId) });
            }
        }
    }
}