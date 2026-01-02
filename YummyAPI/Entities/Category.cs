using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool CategoryStatus { get; set; }
<<<<<<< HEAD
=======

>>>>>>> 6f2ea93 (Entities ve DTOs elave edildi)
        public List<Product>? Products {get;set;}
    }
}