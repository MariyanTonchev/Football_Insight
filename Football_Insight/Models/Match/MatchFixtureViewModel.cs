namespace Football_Insight.Models.Match
{
    public class MatchFixtureViewModel
    {
        public int Id { get; set; }

        public string HomeTeam { get; set; } = string.Empty;

        public string AwayTeam { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;
    }
}
