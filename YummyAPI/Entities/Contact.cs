using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
<<<<<<< HEAD
           public DateTime GetDate { get; set; }=DateTime.Now;
        public bool IsRead { get; set; } = false;

=======
        public bool IsRead {get;set;}
>>>>>>> 6f2ea93 (Entities ve DTOs elave edildi)
    }
}