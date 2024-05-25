using DashBoard.API.Models.Domain;

namespace DashBoard.API.Models.DTO
{
    public class EmploymentWorkingTimeDto
    {
        public decimal EmploymentWorkingTimeId { get; set; }
        public decimal? EmploymentId { get; set; }
        public DateOnly? YearWorking { get; set; }
        public decimal? MonthWorking { get; set; }
        public decimal? NumberDaysActualOfWorkingPerMonth { get; set; }
        public decimal? TotalNumberVacationWorkingDaysPerMonth { get; set; }
    }
}
