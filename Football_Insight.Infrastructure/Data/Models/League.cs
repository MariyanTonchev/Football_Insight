using System.ComponentModel.DataAnnotations;
using Football_Insight.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Infrastructure.Data.Models
{
    [Comment("Represent information about league.")]
    public class League
    {
        [Key]
        [Comment("The unique identifier for the league.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.LeagueNameMaxLength)]
        [Comment("The name of the league.")]
        public string Name { get; set; } = string.Empty;

        //A collection of teams that are part of the league. This represents a one-to-many relationship between League and Team.
        public ICollection<Team> Teams { get; set; } = new List<Team>();

        //A collection of matches that are played within the league. This represents a one-to-many relationship between League and Match.
        public ICollection<Match> Match { get; set; } = new List<Match>();
    }
}