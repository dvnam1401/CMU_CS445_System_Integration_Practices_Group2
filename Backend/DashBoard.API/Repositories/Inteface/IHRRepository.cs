using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IHRRepository
    {
        Task<CreateEmployeeDto> CreateEployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<IEnumerable<BenefitPlan>> FindAllBenefitPlan();
        Task<HRUpdateEmployeeDto> UpdateEmployeeAsync(HRUpdateEmployeeDto updateEmployeeDto);
        Task<HRUpdateEmployeeDto> FindEmployee(decimal employmentId);
    }
}
