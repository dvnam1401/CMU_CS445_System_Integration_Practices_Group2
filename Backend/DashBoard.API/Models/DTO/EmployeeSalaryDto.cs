using DashBoard.API.Models.Domain;
using DashBoard.API.Models.Inteface;

namespace DashBoard.API.Models.DTO
{
    public class EmployeeSalaryDto : IEmployeeData
    {
        public bool? ShareholderStatus { get; set; }
        public string? FullName { get; set; }
        public bool? Gender { get; set; }
        public string? Category { get; set; } // category

        public string? Ethnicity { get; set; }
        public List<JobHistory>? JobHistories { get; set; }
        public Employment? Employment { get; set; }
        public decimal? TotalSalary { get; set; }
    }
}
