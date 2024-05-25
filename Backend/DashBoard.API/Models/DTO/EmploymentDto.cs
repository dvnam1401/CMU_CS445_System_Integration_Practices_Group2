namespace DashBoard.API.Models.DTO
{
    public class EmploymentDto
    {
        public decimal EmploymentId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Department { get; set; }       
        public DateOnly? Birthday { get; set; }
        public DateOnly? HireDateForWorking { get; set; }
        public DateOnly? RehireDateForWorking { get; set; }

    }
}
