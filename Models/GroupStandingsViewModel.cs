using System.Collections.Generic;

namespace WorldCup2026.Models
{
    public class TeamStanding
    {
        public Team Team { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference => GoalsFor - GoalsAgainst;
        public int Points => (Wins * 3) + Draws;
    }

    public class WorldCupViewModel
    {
        public Dictionary<string, List<TeamStanding>> Groups { get; set; }
        public List<Match> Matches { get; set; }
    }
}