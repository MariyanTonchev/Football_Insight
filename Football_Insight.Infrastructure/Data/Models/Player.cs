using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Models
{
    [Comment("Represent information about the player.")]
    public class Player
    {
        [Key]
        [Comment("The unique identifier for the player.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(PersonFirstNameMaxLength)]
        [Comment("The player's first name.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(PersonLastNameMaxLength)]
        [Comment("The player's last name.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Comment("The date of birth of the player.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Precision(TotalDigitsDecimalPrecision, DigitsAfterDecimalPoint)]
        [Comment("The transfer or market price of the player.")]
        public decimal Price { get; set; }

        [Required]
        [Precision(TotalDigitsDecimalPrecision, DigitsAfterDecimalPoint)]
        [Comment("The salary of the player.")]
        public decimal Salary { get; set; }

        [Required]
        [Comment("Foreign key to the Team entity. Represents the team to which the player currently belongs.")]
        public int TeamId { get; set; }

        [Required]
        [Comment("The position of the player on the field.")]
        public int Position { get; set; }

        [ForeignKey(nameof(TeamId))]
        //Navigation property for the Team entity.
        public Team Team { get; set; } = null!;

        //Collection of goals scored by the player.
        public ICollection<Goal> GoalsScored { get; set; } = new List<Goal>();

        //Collection of goals where the player was the assisting player.
        public ICollection<Goal> GoalsAssisted { get; set; } = new List<Goal>();
    }
}
