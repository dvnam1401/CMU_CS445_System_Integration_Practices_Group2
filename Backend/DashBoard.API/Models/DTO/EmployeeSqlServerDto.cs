using DashBoard.API.Models.Domain;

namespace DashBoard.API.Models.DTO
{
    public class EmployeeSqlServerDto
    {
        public uint EmployeeId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public bool? ShareholderStatus { get; set; }
        public bool? Gender { get; set; }
        public string? Ethnicity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Employment Employments { get; set; }
        public List<JobHistory>? JobHistories { get; set; }
    }
}
