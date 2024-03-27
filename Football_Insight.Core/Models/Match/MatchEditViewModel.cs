using Football_Insight.Core.Models.Team;
using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Core.Models.Match
{
    public class MatchEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; } = DateTime.Today;

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        public ICollection<TeamSimpleViewModel> Teams { get; set; } = new List<TeamSimpleViewModel>();
    }
}
