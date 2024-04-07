
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IServiceRepository
    {
        Task<BenefitPlan?> GetBenefitPlanById(uint benefit);
        Task<IEnumerable<BenefitPlan?>> GetByAll();
        Task<IEnumerable<Employee?>> GetByAllEmployee();
        Task<IEnumerable<EmployeeSalaryDto>> GetEmployeesSalary(EmployeeFilterDto filter);
        Task<IEnumerable<NumberOfVacationDay>> GetNumberOfVacationDays(EmployeeFilterDto filter);
        Task<IEnumerable<EmployeeAnniversaryDto>> GetEmployeesAnniversaryInfo(int daysLimit);
    }
}
