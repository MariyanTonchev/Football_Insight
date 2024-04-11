using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Models
{
    [Comment("Represent information about the stadium.")]
    public class Stadium
    {
        [Key]
        [Comment("The unique identifier for the stadium.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(StadiumNameMaxLength)]
        [Comment("The name of the stadium.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(StadiumAddressMaxLength)]
        [Comment("The physical address of the stadium.")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Range(StadiumMinRange, StadiumMaxRange)]
        [Comment("The seating capacity of the stadium.")]
        public int Capacity { get; set; }

        [Required]
        [Comment("The year when the stadium was built.")]
        public int YearBuilt { get; set; }

        //A collection of teams that consider this stadium their home.
        public ICollection<Team> Teams { get; set; } = new List<Team>();

        //[A collection of matches that are scheduled to be played in this stadium.
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
