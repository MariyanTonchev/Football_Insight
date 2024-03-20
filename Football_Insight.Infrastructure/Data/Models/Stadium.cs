using System.ComponentModel.DataAnnotations;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Models
{
    public class Stadium
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(StadiumNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(StadiumAddressMaxLength)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int YearBuilt { get; set; }

        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
