using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Core.Models.Player
{
    public class PlayerFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Selecting a position is required.")]
        public int SelectedPosition { get; set; }

        public ICollection<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
    }
}
