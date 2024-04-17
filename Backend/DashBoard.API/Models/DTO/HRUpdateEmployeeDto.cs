using DashBoard.API.Models.Inteface;

namespace DashBoard.API.Models.DTO
{
    public class HRUpdateEmployeeDto : EmployeeDtoBase
    {
        public decimal EmploymentId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public short? ShareholderStatus { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
