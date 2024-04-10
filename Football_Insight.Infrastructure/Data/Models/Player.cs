﻿using Football_Insight.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PersonFirstNameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(PersonLastNameMaxLength)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Precision(TotalDigitsDecimalPrecision, DigitsAfterDecimalPoint)]
        public decimal Price { get; set; }

        [Required]
        [Precision(TotalDigitsDecimalPrecision, DigitsAfterDecimalPoint)]
        public decimal Salary { get; set; }

        [Required]
        public int TeamId { get; set; }

        public int Position { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; } = null!;

        public ICollection<Goal> GoalsScored { get; set; } = new List<Goal>();
        public ICollection<Goal> GoalsAssisted { get; set; } = new List<Goal>();
    }
}
