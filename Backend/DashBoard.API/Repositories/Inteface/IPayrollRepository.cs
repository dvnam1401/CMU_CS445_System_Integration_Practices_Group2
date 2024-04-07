
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<PayRate>> GetAllPayRates();
        Task<EmployeeDto?> GetEmployeeById(int id);
        Task<PayRollUpdateEmployeeDto?> UpdateEmployeeAsync(PayRollUpdateEmployeeDto? employee);
    }
}
