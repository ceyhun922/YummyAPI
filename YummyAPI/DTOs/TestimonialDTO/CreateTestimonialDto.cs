namespace YummyAPI.DTOs.TestimonialDTO
{
    public class CreateTestimonialDto
    {
        public string? TestimonialName { get; set; }
        public string? TestimonialTitle { get; set; }
        public string? TestimonialMessage { get; set; }
        public string? TestimonialImageUrl { get; set; }
        public bool TestimonialStatus { get; set; }
    }
}