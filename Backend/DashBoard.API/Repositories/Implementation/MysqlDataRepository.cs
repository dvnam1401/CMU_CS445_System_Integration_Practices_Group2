using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Models.Inteface;
using DashBoard.API.Repositories.Inteface;
using Google.Protobuf.WellKnownTypes;
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

        // lấy ra danh sách benefit
        public async Task<IEnumerable<PayRate?>> GetByAllPayRate()
        {
            return await mysqlContext.Payrates.ToListAsync();
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
            //PayRate = decimal.TryParse(e.PayRate, out decimal result) ? result : 0M,
            PayRate = ParseDecimalOrDefault(e.PayRate),
            PaidToDate = e.PaidToDate,
            PayRatesIdPayRates = e.PayRatesIdPayRatesNavigation,
        }).ToListAsync();
            return query;
            //}
        }

        private static decimal ParseDecimalOrDefault(string value)
        {
            // Thử chuyển đổi chuỗi thành decimal, nếu không thành công thì trả về 0
            return decimal.TryParse(value, out decimal result) ? result : 0M;
        }

        public async Task<IEnumerable<Employee?>> GetByAllEmployee()
        {
            return await mysqlContext.Employees.ToListAsync();
        }
    }
}
