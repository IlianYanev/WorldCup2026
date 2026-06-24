namespace WorldCup2026.Models
{
    public class ThirdPlacePrediction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public int Rank { get; set; }
        public virtual Team Team { get; set; }
    }
}