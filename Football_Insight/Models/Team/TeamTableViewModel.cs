﻿namespace Football_Insight.Models.Team
{
    public class TeamTableViewModel
    {
        public int Id { get; set; }

        public int Position { get; set; }

        public string Logo {  get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Draws { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int Points { get; set; }

        //Breadcrumb purpose
        public string League { get; set; } = string.Empty;
    }
}
