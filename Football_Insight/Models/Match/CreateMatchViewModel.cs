using Football_Insight.Models.Team;
using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Models.Match
{
    public class CreateMatchViewModel : IValidatableObject
    {
        [Required]
        public DateTime DateTime { get; set; } = DateTime.Today;

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int LeagueId { get; set; }

        public ICollection<SimpleTeamViewModel> Teams { get; set; } = new List<SimpleTeamViewModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HomeTeamId == 0)
            {
                yield return new ValidationResult(
                    "Home Team is required! Please select!",
                    new[] { "HomeTeamId" });
            }

            if (AwayTeamId == 0)
            {
                yield return new ValidationResult(
                    "Away Team is required! Please select!",
                    new[] { "AwayTeamId" });
            }

            if (HomeTeamId == AwayTeamId)
            {
                yield return new ValidationResult(
                    "Home Team and Away Team cannot be the same.",
                    new[] { "HomeTeamId", "AwayTeamId" });
            }
        }
    }
}