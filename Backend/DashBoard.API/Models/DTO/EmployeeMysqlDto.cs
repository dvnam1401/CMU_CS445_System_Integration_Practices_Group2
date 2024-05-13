using DashBoard.API.Models.Domain;

namespace DashBoard.API.Models.DTO
{
    public class EmployeeMysqlDto
    {
        public int EmployeeId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public PayRate? PayRatesIdPayRates { get; set; }
        public decimal? PayRate { get; set; }      
        public decimal? PaidToDate { get; internal set; }
    }
}
