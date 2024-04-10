using Football_Insight.Infrastructure.Data.Enums;

namespace Football_Insight.Core.Models.Player
{
    public class PlayerSquadViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public decimal Price { get; set; }

        public decimal Salary { get; set; }

        public PlayerPosition Position { get; set; }

        public int GoalScored { get; set; }

        public int GoalAssited { get; set; }
    }
}