using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface ISummarizedRepository
    {
        List<string> GetAllDepartments();
        Task<IEnumerable<EmployeeSalaryDto>> GetEmployeesSalary(EmployeeFilterDto filter);
        Task<IEnumerable<NumberOfVacationDay>> GetNumberOfVacationDays(EmployeeFilterDto filter);
        Task<IEnumerable<EmployeeAverageBenefitDto>> GetEmployeeAverageBenefit(EmployeeFilterDto filter);
    }
}
