namespace WorldCup2026.Models
{
    public class KnockoutPrediction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MatchId { get; set; } // 73 до 88
        public int WinnerTeamId { get; set; }
        public string Stage { get; set; } // "R32", "R16", "QF", "SF", "Final"
        public virtual Team WinnerTeam { get; set; }
    }
}