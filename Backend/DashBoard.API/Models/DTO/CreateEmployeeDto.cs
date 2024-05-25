using DashBoard.API.Models.Inteface;

namespace DashBoard.API.Models.DTO
{
    public class CreateEmployeeDto
    {
        public int EmploymentId { get; set; }
        public string? EmploymentCode { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }        
        public decimal Ssn { get; set; }       
        public string? EmploymentStatus { get; set; }
        public DateOnly? HireDateForWorking { get; set; }
        public decimal? NumberDaysRequirementOfWorkingPerMonth { get; set; }
        public string? Department { get; set; }        
        public int PayRatesIdPayRates { get; set; }
        public string? PayRate { get; set; }
        public int PersonalId { get; set; }        
    }
}
