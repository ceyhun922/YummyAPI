namespace YummyAPI.DTOs.CategoryDTO
{
    public class GetCategoryByIdDto
    {
         public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool CategoryStatus { get; set; }

    }
}