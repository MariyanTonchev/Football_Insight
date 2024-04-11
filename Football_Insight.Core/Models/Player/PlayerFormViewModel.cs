using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static Football_Insight.Infrastructure.Constants.DataConstants;
using static Football_Insight.Core.Constants.ValidationMessages;

namespace Football_Insight.Core.Models.Player
{
    public class PlayerFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(PersonFirstNameMaxLength, MinimumLength = PersonFirstNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(PersonLastNameMaxLength, MinimumLength = PersonLastNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        [Display(Name = "Team")]
        public int TeamId { get; set; }

        [Required]
        [Display(Name = "Position")]
        public int SelectedPosition { get; set; }

        public ICollection<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
    }
}
