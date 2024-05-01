using DashBoard.API.Data;
using DashBoard.API.Models.Domain;

//using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly MysqlContext mysqlContext;
        private readonly SqlServerContext sqlServerContext;

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
                .Where(x => x.IdEmployee == id)
                .Select(x => new EmployeeDto
                {
                    EmployeeId = x.IdEmployee,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Ssn = x.Ssn,
                    PayRate = x.PayRate,
                    PayRatesIdPayRates = x.PayRatesIdPayRates,
                    PayRatesName = x.PayRatesIdPayRatesNavigation.PayRateName,
                    //VacationDays = x.VacationDays,
                    //PaidToDate = x.PaidToDate,
                    //PaidLastYear = x.PaidLastYear,
                    //PayRates = x.PayRatesIdPayRatesNavigation
                })
                .FirstOrDefaultAsync();
            return employeeDto;
        }

        public async Task<PayRollUpdateEmployeeDto?> UpdateEmployeeAsync(PayRollUpdateEmployeeDto? employee)
        {
            var existingEmployee = await mysqlContext.Employees.FirstOrDefaultAsync(x => x.IdEmployee == employee.EmployeeId);
            if (existingEmployee is not null)
            {
                mysqlContext.Entry(existingEmployee).
                    CurrentValues.SetValues(employee);
                await mysqlContext.SaveChangesAsync();
                return employee;
            }
            await UpdateEmployeeInSqlServer(employee);


            return null;
        }
        private async Task UpdateEmployeeInMySql(HRUpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await mysqlContext.Employees
                //.Include(x => x.Birthday)
                .FirstOrDefaultAsync(x => x.IdEmployee == updateEmployeeDto.EmploymentId);
            if (employee is not null)
            {
                employee.FirstName = updateEmployeeDto.FirstName;
                employee.LastName = updateEmployeeDto.LastName;

                await mysqlContext.SaveChangesAsync();
            }
        }

        private async Task UpdateEmployeeInSqlServer(PayRollUpdateEmployeeDto? employee)
        {
            var existingEmployee = await sqlServerContext.Employments
                .FirstOrDefaultAsync(x => x.EmploymentId == employee.EmployeeId);

            if (existingEmployee is not null)
            {
                existingEmployee.Personal.CurrentLastName = employee.LastName;
                existingEmployee.Personal.CurrentFirstName = employee.FirstName;               
                await sqlServerContext.SaveChangesAsync();
            }
        }
    }
}
