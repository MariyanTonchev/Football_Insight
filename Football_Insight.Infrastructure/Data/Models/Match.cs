using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Football_Insight.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Infrastructure.Data.Models
{
    [Comment("Represent information about the match.")]
    public class Match
    {
        [Key]
        [Comment("The unique identifier for the match.")]
        public int Id { get; set; }

        [Required]
        [Comment("The date and time when the match is scheduled to take place.")]

        public DateTime DateAndTime { get; set; }

        [Required]
        [Comment("Foreign key representing the home team. References the Team entity.")]
        public int HomeTeamId { get; set; }

        [Required]
        [Comment("Foreign key representing the away team. References the Team entity.")]
        public int AwayTeamId { get; set; }

        [Required]
        [Comment("Foreign key for the stadium where the match is held. References the Stadium entity.")]
        public int StadiumId { get; set; }

        [Required]
        [Comment("The score of the home team at the end of the match.")]
        public int HomeScore { get; set; }

        [Required]
        [Comment("The score of the away team at the end of the match.")]
        public int AwayScore { get; set; }

        [Required]
        [Comment("Foreign key for the league to which this match belongs. References the League entity.")]
        public int LeagueId { get; set; }

        [Required]
        [Comment("The current status of the match.")]
        public MatchStatus Status { get; set; }

        [Required]
        [Comment("The total minutes played in the match.")]
        public int Minutes {  get; set; }

        [ForeignKey(nameof(HomeTeamId))]
        //Navigation property for the home team.
        public virtual Team HomeTeam { get; set; } = null!;

        [ForeignKey(nameof(AwayTeamId))]
        //Navigation property for the away team.
        public virtual Team AwayTeam { get; set; } = null!;

        [ForeignKey(nameof(StadiumId))]
        //Navigation property for the stadium.
        public virtual Stadium Stadium { get; set; } = null!;

        [ForeignKey(nameof(LeagueId))]
        //Navigation property for the league.
        public virtual League League { get; set; } = null!;

        //A collection of goals scored in the match.
        public ICollection<Goal> Goals = new List<Goal>();

        //A collection of users who have marked this match as a favorite.
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}