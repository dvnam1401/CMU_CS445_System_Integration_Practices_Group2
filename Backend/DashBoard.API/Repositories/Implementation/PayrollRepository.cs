using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly MysqlContext mysqlContext;

        public PayrollRepository(MysqlContext mysqlContext)
        {
            this.mysqlContext = mysqlContext;
        }
        public async Task<IEnumerable<PayRate>> GetAllPayRates()
        {
            return await mysqlContext.Payrates.ToListAsync();
        }

        public async Task<EmployeeDto?> GetEmployeeById(int id)
        {
            var employeeDto = await mysqlContext.Employees
                .Where(x => x.EmployeeNumber == id)
                .Select(x => new EmployeeDto
                {
                    EmployeeNumber = x.EmployeeNumber,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Ssn = x.Ssn,
                    PayRate = x.PayRate,
                    PayRatesIdPayRates = x.PayRatesIdPayRates,
                    VacationDays = x.VacationDays,
                    PaidToDate = x.PaidToDate,
                    PaidLastYear = x.PaidLastYear,
                    PayRates = x.PayRatesIdPayRatesNavigation
                })
                .FirstOrDefaultAsync();
            return employeeDto;
        }

        public async Task<PayRollUpdateEmployeeDto?> UpdateEmployeeAsync(PayRollUpdateEmployeeDto? employee)
        {
            var existingEmployee = await mysqlContext.Employees.FirstOrDefaultAsync(x => x.EmployeeNumber == employee.EmployeeNumber);
            if (existingEmployee is not null)
            {
                mysqlContext.Entry(existingEmployee).
                    CurrentValues.SetValues(employee);
                await mysqlContext.SaveChangesAsync();
                return employee;
            }
            return null;
        }
    }
}
