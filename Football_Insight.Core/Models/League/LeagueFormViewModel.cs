using System.ComponentModel.DataAnnotations;
using static Football_Insight.Infrastructure.Constants.DataConstants;
using static Football_Insight.Core.Constants.ValidationMessages;

namespace Football_Insight.Core.Models.League
{
    public class LeagueFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(LeagueNameMaxLength, MinimumLength = LeagueNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;
    }
}
