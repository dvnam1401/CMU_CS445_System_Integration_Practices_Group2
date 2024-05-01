using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IServiceRepository
    {
        List<string> GetAllDepartments();
        Task<BenefitPlan?> GetBenefitPlanById(decimal BenefitPlansId);
        Task<IEnumerable<BenefitPlan?>> GetByAllBenefitPlan();
        Task<IEnumerable<Employee?>> GetByAllEmployee();
        Task<IEnumerable<EmployeeSalaryDto>> GetEmployeesSalary(EmployeeFilterDto filter);
        Task<IEnumerable<NumberOfVacationDay>> GetNumberOfVacationDays(EmployeeFilterDto filter);
        Task<IEnumerable<EmployeeAnniversaryDto>> GetEmployeesAnniversaryInfo(int daysLimit);
        Task<IEnumerable<EmployeeAverageBenefitDto>> GetEmployeeAverageBenefit(EmployeeFilterDto filter);
        Task<IEnumerable<EmployeeVacationDto>> GetEmployeesWithAccumulatedVacationDays(int minimumDays);
        Task<IEnumerable<EmployeeBirthdayDto>> GetEmployeesWithBirthdaysThisMonth(int daysLimit);
    }
}
