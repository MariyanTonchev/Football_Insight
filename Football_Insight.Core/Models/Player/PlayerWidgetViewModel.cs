using Football_Insight.Infrastructure.Data.Enums;

namespace Football_Insight.Core.Models.Player
{
    public class PlayerWidgetViewModel
    {
        public int PlayerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Team { get; set; } = string.Empty;

        public PlayerPosition Position { get; set; }

        public string League { get; set; } = string.Empty;

        public int GoalsContributed {  get; set; }

    }
}
