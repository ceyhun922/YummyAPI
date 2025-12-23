namespace YummyAPI.DTOs.ChefDTO
{
    public class GetByIdChefDto
    {
        public int ChefId { get; set; }
        public string? ChefName { get; set; }
        public string? ChefTitle { get; set; }
        public string? ChefDescription { get; set; }
        public string? ChefImageUrl { get; set; }
        public string? ChefFacebookUrl { get; set; }
        public string? ChefXUrl { get; set; }
        public string? ChefInstagramUrl { get; set; }
        public string? ChefLinkedinUrl { get; set; }
        public bool ChefStatus { get; set; }
    }
}