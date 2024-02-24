using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Data.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Player> Players { get; set;} = new List<Player>();
    }
}
