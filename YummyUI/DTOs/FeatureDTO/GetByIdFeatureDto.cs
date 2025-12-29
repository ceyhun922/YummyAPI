namespace YummyUI.DTOs.FeatureDTO
{
    public class GetByIdFeatureDto
    {
        public int FeatureId { get; set; }
        public string? FeatureTitle { get; set; }
        public string? FeatureTitle2 { get; set; }
        public string? FeatureDescription { get; set; }
        public string? FeatureImageUrl { get; set; }
        public bool FeatureStatus { get; set; }
    }
}