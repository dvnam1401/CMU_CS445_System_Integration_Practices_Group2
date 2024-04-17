using DashBoard.API.Models.Domain;
using DashBoard.API.Models.Inteface;

namespace DashBoard.API.Models.DTO
{
    public class NumberOfVacationDay : IEmployeeData
    {
        public short? ShareholderStatus { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? Category { get; set; } // full time or parttime

        public string? Ethnicity { get; set; }
        public List<JobHistoryDto>? JobHistories { get; set; } 
        public decimal? TotalDaysOff { get; set; }
    }
}
