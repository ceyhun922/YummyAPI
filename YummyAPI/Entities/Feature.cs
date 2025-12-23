using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }
        public string? FeatureTitle {get;set;}
        public string? FeatureTitle2 {get;set;}
        public string? FeatureDescription {get;set;}
        public string? FeatureImageUrl {get;set;}
        public bool FeatureStatus { get; set; }
    }
}