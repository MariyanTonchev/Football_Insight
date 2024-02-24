using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Data.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}