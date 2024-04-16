namespace Football_Insight.Core.Models.Goal
{
    public class GoalSimpleModelView
    {
        public int GoalId { get; set; }

        public string ScorerName { get; set; } = string.Empty;

        public string AssistantName { get; set; } = string.Empty;

        public int GoalTime { get; set; }

        public int TeamId { get; set; }
    }
}
