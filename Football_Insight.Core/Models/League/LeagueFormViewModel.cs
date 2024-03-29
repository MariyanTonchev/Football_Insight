using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Core.Models.League
{
    public class LeagueFormViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
