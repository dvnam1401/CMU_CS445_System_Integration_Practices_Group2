using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Models.Inteface;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class MysqlDataRepository : IMysqlDataRepository
    {
        public readonly MysqlContext? mysqlContext;

        public MysqlDataRepository(MysqlContext? mysqlContext)
        {
            this.mysqlContext = mysqlContext;
        }

        public async Task<IEnumerable<EmployeeMysqlDto>> FetchMysqlEmployeeDataAsync()
        {
            //using (var mysqlContext = new MysqlContext()) // Tạo phiên bản DbContext mới
            //{
            var query = await mysqlContext.Employees
            .Select(e => new EmployeeMysqlDto
            {
                EmployeeId = e.IdEmployee,
                LastName = e.LastName,
                FirstName = e.FirstName,
                PayRate = decimal.Parse(e.PayRate),
                PaidToDate = e.PaidToDate,
                PayRatesIdPayRates = e.PayRatesIdPayRatesNavigation,
            }).ToListAsync();
            return query;
            //}
        }
        public async Task<IEnumerable<Employee?>> GetByAllEmployee()
        {
            return await mysqlContext.Employees.ToListAsync();
        }
    }
}
