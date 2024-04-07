using DashBoard.API.Models.Domain;

namespace DashBoard.API.Models.DTO
{
    public class EmployeeMysqlDto
    {
        public uint EmployeeNumber { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? PayRate { get; set; }
        public decimal? PaidToDate { get; set; }
        public decimal? PaidLastYear { get; set; }
        public PayRate? PayRatesIdPayRates { get; set; }
        public List<VacationMysqlDto>? Vacations { get; set; } = new List<VacationMysqlDto>();
        public int TotalVacationsCount { get; set; }
    }
}
