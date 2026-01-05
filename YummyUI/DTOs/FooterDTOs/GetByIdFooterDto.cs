namespace YummyUI.DTOs.FooterDTOs
{
    public class GetByIdFooterDto
    {
         public int FooterId { get; set; }
        public string? MapLocation { get; set; }
        public string? Address { get; set; }
        public string? CallUs { get; set; }
        public string? EmailUs { get; set; }
        public string? OpeningHours { get; set; }
    }
}