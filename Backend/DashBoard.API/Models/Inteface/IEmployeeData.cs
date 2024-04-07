

using DashBoard.API.Models.Domain;

namespace DashBoard.API.Models.Inteface
{
    public interface IEmployeeData
    {
        bool? ShareholderStatus { get; set; }
        string FullName { get; set; }
        bool? Gender { get; set; }
        string Category { get; set; }
        string Ethnicity { get; set; }
        List<JobHistory> JobHistories { get; set; }
    }
}
