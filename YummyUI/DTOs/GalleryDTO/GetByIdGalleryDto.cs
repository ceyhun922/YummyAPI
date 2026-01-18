namespace YummyUI.DTOs.GalleryDTO
{
    public class GetByIdGalleryDto
    {
          public int GalleryId { get; set; }
        public string? GalleryTitle { get; set; }
        public string? GalleryImageUrl { get; set; }
        public bool GallerySatatus { get; set; }
    }
}