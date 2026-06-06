using WorldCup2026.Models;

namespace WorldCup2026.Models
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public Team Home { get; set; }
        public Team Away { get; set; }
    }
}