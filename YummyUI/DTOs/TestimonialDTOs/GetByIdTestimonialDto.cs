namespace YummyUI.DTOs.TestimonialDTOs
{
    public class GetByIdTestimonialDto
    {
          public int TestimonialId { get; set; }
        public string? TestimonialName { get; set; }
        public string? TestimonialTitle { get; set; }
        public string? TestimonialMessage { get; set; }
        public string? TestimonialImageUrl { get; set; }
        public bool TestimonialStatus { get; set; }
    }
}