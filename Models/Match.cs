using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldCup2026.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HomeTeamId { get; set; }
        
        [ForeignKey("HomeTeamId")]
        public Team HomeTeam { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [ForeignKey("AwayTeamId")]
        public Team AwayTeam { get; set; }

        [Required]
        public int StadiumId { get; set; }

        [ForeignKey("StadiumId")]
        public Stadium Stadium { get; set; }

        [Required]
        public DateTime KickOffTime { get; set; }

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}