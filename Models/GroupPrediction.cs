namespace WorldCup2026.Models
{
    public class GroupPrediction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public int PredictedPosition { get; set; } // 1, 2, 3 или 4
        
        public virtual Team Team { get; set; }
    }
}