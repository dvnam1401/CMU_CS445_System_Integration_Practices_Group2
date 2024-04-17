namespace DashBoard.API.Models.DTO
{
    public class JobHistoryDto
    {
        public decimal? EmploymentId { get; set; }
        public string? Department { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ThruDate { get; set; }
        public string? JobTitle { get; set; }
        public string? Location { get; set; }
        public short? TypeOfWork { get; set; }

    }
}
