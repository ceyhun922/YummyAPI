using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class Footer
    {
        [Key]
        public int FooterId { get; set; }
        public string? MapLocation { get; set; }
        public string? Address { get; set; }
        public string? CallUs { get; set; }
        public string? EmailUs { get; set; }
        public string? OpeningHours { get; set; }
        public string? FaceookUrl { get; set; }
        public string? XUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? LinkedinUrl { get; set; }

    }
}