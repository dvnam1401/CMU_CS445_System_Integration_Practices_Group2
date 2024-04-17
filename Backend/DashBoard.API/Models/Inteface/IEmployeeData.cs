

using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Models.Inteface
{
    public interface IEmployeeData
    {
        short? ShareholderStatus { get; set; }
        string FullName { get; set; }
        string? Gender { get; set; }
        string Category { get; set; }
        string Ethnicity { get; set; }
        List<JobHistoryDto> JobHistories { get; set; }
    }
}
