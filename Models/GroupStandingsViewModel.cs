namespace WorldCup2026.Models
{
    public class GroupStandingsViewModel
    {
        public Dictionary<string, List<TeamStandingViewModel>> Groups { get; set; } = new();
    }

    public class TeamStandingViewModel
    {
        public Team Team { get; set; } = new();
        public int Points { get; set; }
        public int GoalDifference { get; set; }
    }
}