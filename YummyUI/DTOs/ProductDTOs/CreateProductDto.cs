namespace YummyUI.DTOs.ProductDTOs
{
    public class CreateProductDto
    {
        public string? ProductTitle { get; set; }
        public string? ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public string? ProductImageUrl { get; set; }
        public bool ProductStatus { get; set; }
        public int CategoryId { get; set; }
    }
}