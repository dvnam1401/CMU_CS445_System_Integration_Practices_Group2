using DashBoard.API.Models.Inteface;

namespace DashBoard.API.Models.DTO
{
    public class CreateEmployeeDto : EmployeeDtoBase
    {
        public int PersonalId { get; set; }
        public int BenefitPlanId { get; set; }
        public int EmploymentId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }        
        public int PayRatesIdPayRates { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Ssn { get; set; }
        public short? ShareholderStatus { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }      
        //public string? City { get; set; }
        public string? Gender { get; set; }
        public string? Department { get; set; }
    }
}
