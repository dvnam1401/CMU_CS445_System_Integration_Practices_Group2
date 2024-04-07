using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface ISqlDataRepository
    {
        Task<IEnumerable<BenefitPlan?>> GetByAll();
        Task<BenefitPlan?> GetBenefitPlanById(uint benefit);
        Task<List<EmployeeSqlServerDto>> FetchSqlServerData();
        List<EmployeeSqlServerDto> FilterJobHistorySqlServe(List<EmployeeSqlServerDto> data, EmployeeFilterDto filter);
    }
}
