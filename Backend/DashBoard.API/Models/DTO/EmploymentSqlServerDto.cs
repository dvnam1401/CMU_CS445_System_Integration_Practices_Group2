using DashBoard.API.Models.Domain;

namespace DashBoard.API.Models.DTO
{
    public class EmploymentSqlServerDto
    {
        public decimal EmploymentId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public short? ShareholderStatus { get; set; }
        public string? Gender { get; set; }
        public string? Ethnicity { get; set; }
        public string? EmploymentStatus { get; set; }
        public DateOnly? HireDateForWorking { get; set; }
        public decimal? NumberDaysRequirementOfWorkingPerMonth { get; set; }
        public decimal? BenefitPlan { get; set; }
        public Personal? Personal { get; set; }
        public List<EmploymentWorkingTimeDto>? WorkingTime { get; set; }
        public List<JobHistoryDto>? JobHistories { get; set; }        
    }
}
