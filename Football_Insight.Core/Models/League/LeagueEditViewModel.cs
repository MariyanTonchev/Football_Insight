using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Core.Models.League
{
    public class LeagueEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
