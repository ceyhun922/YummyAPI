namespace YummyUI.DTOs.AboutDTO
{
    public class GetAboutByIdDto
    {
        public int AboutId { get; set; }
        public string? AboutTitle { get; set; }
        public string? AboutSubTitle { get; set; }
        public string? AboutTitleChecked1 { get; set; }
        public string? AboutTitleChecked2 { get; set; }
        public string? AboutTitleChecked3 { get; set; }
        public string? AboutDesciription { get; set; }
        public string? AboutImageUrl { get; set; }
        public string? AboutVideoUrl { get; set; }
    }
}