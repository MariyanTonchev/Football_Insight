﻿using System.ComponentModel.DataAnnotations;
using static Football_Insight.Data.DataConstants;

namespace Football_Insight.Data.Models
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
