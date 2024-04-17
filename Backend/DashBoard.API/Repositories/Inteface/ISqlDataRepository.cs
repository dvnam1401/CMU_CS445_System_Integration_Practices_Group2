using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface ISqlDataRepository
    {
        Task<IEnumerable<BenefitPlan?>> GetByAll();
        Task<BenefitPlan?> GetBenefitPlanById(decimal BenefitPlansId);
        Task<List<EmploymentSqlServerDto>> FetchSqlServerData(EmployeeFilterDto filter);
        List<EmploymentSqlServerDto> FilterJobHistorySqlServe(List<EmploymentSqlServerDto> data, EmployeeFilterDto filter);
    }
}
