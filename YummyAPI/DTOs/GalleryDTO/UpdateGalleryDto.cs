namespace YummyAPI.DTOs.GalleryDTO
{
    public class UpdateGalleryDto
    {
        public int GalleryId { get; set; }
        public string? GalleryTitle { get; set; }
        public string? GalleryImageUrl { get; set; }
        public bool GallerySatatus { get; set; }
    }
}