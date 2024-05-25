namespace DashBoard.API.Models.DTO
{
    public class ViewTotal
    {       
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? Category { get; set; } // full time or parttime
        public short? ShareholderStatus { get; set; }
        public string? Ethnicity { get; set; }
        public string? Department { get; set; }
        public decimal? Total { get; set; }
    }
}
