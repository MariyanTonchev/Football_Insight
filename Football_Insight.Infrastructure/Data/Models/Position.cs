using System.ComponentModel.DataAnnotations;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PositionNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Player> Players { get; set;} = new List<Player>();
    }
}
