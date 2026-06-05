using System.ComponentModel.DataAnnotations;

namespace WorldCup2026.Models
{
    public class Stadium
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}