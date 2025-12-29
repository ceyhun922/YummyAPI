namespace YummyAPI.DTOs.ProductDTO
{
    public class ResultGetAllProductWithCategoryDto
    {
        public int ProductId { get; set; }
        public string? ProductTitle { get; set; }
        public string? ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public string? ProductImageUrl { get; set; }
        public bool ProductStatus { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}