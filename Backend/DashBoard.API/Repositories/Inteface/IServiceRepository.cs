using DashBoard.API.Models;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IServiceRepository
    {
        Task<BenefitPlan?> GetById(int benefit);
        Task<IEnumerable<BenefitPlan?>> GetByAll();
        Task<IEnumerable<Employee?>> GetByAllEmployee();
        Task<IEnumerable<EmployeeSalaryDto>> GetEmployeesByFilter(EmployeeFilterDto filter);
        //Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeAllSalary();
        //Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeSalaryByGender(bool Gender);
        //Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeSalaryEthnicity(string ethnicity);
        //Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeSalaryByCategory(string category);
        //Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeSalaryByDepartment(string department);
    }
}
