using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Models
{
    [Comment("Represent information about the team.")]
    public class Team
    {
        [Key]
        [Comment("The unique identifier for the team.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        [Comment("The name of the team.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("The year the team was founded.")]
        public int Founded { get; set; }

        [Required]
        [Comment("URL to the team logo image.")]
        public string LogoURL { get; set; } = string.Empty;

        [Required]
        [MaxLength(CoachNameMaxLength)]
        [Comment("The name of the team current coach.")]
        public string Coach { get; set; } = null!;

        [Required]
        [Comment("Foreign key to the League entity. Indicates the league in which the team competes.")]
        public int LeagueId { get; set; }

        [Required]
        [Comment("Foreign key to the Stadium entity. Represents the home stadium of the team.")]
        public int StadiumId { get; set; }

        [ForeignKey(nameof(LeagueId))]
        //Navigation property for the League entity.
        public virtual League League { get; set; } = null!;

        [ForeignKey(nameof(StadiumId))]
        //Navigation property for the Stadium entity.
        public virtual Stadium Stadium { get; set; } = null!;

        //A collection of goals scored by this team.
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();

        //A collection of players who are part of this team.
        public ICollection<Player> Players { get; set; } = new List<Player>();

        //A collection of matches where this team is listed as the home team.
        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();

        //A collection of matches where this team is listed as the away team.
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    }
}