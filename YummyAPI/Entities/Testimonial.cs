using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{

    public class Testimonial
    {
        [Key]
        public int TestimonialId { get; set; }
        public string? TestimonialName { get; set; }
        public string? TestimonialTitle { get; set; }
        public string? TestimonialMessage { get; set; }
        public bool TestimonialStatus { get; set; }
    }
}