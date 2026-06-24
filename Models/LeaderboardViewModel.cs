using System.Collections.Generic;

namespace WorldCup2026.Models
{
    public class LeaderboardViewModel
    {
        public List<UserScoreViewModel> UserScores { get; set; } = new List<UserScoreViewModel>();
    }

    public class UserScoreViewModel
    {
        public string Username { get; set; }
        public int TotalPoints { get; set; }
        public int GroupPoints { get; set; }
        public int PodiumPoints { get; set; }
    }
}