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
            using (var mysqlContext = new MysqlContext()) // Tạo phiên bản DbContext mới
            {
                var query = await mysqlContext.Employees
                .Select(e => new EmployeeMysqlDto
                {
                    EmployeeNumber = e.EmployeeNumber,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    PaidToDate = e.PaidToDate,
                    BirthDate = e.Birthday,
                    PayRatesIdPayRates = e.PayRatesIdPayRatesNavigation,
                }).ToListAsync();
                return query;
            }
        }

        // lấy lịch sử làm việc từ bảng DetailVacation
        public async Task<IEnumerable<VacationMysqlDto>> FeatchMysqlDetailVacationAsync()
        {
            using (var mysqlContext = new MysqlContext()) // Tạo phiên bản DbContext mới
            {
                var query = await mysqlContext.Details
                .Select(d => new VacationMysqlDto
                {
                    EmployeeNumber = d.EmployeeNumber,
                    Dayoff = d.Dayoff,
                    ResignationContent = d.ResignationContent,
                }).ToListAsync();
                return query;
            }
        }

        // kết hợp 2 bảng employee và detail vacation 
        public IEnumerable<EmployeeMysqlDto> FeatchMysql(IEnumerable<EmployeeMysqlDto> employees, IEnumerable<VacationMysqlDto> vacations)
        {
            var vacationGroups = vacations
                .GroupBy(v => v.EmployeeNumber)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var employee in employees)
            {
                if (vacationGroups.TryGetValue(employee.EmployeeNumber, out var employeeVacations))
                {
                    employee.Vacations = employeeVacations;
                }
                else
                {
                    employee.Vacations = new List<VacationMysqlDto>(); // Không có kỳ nghỉ nào, gán danh sách rỗng
                }
            }
            return employees;
        }

        // lọc các lịch sử làm việc 
        public List<EmployeeMysqlDto> FilterEmployeeDataByVacation(IEnumerable<EmployeeMysqlDto> data, EmployeeFilterDto? filter)
        {
            // Chuyển đổi và lọc dữ liệu
            var filteredData = data.Select(e =>
            {
                var filteredVacations = e.Vacations?.Where(vacation =>
                    vacation.Dayoff.HasValue &&
                    (!filter.Year.HasValue || vacation.Dayoff.Value.Year == filter.Year.Value) &&
                    (!filter.Month.HasValue || vacation.Dayoff.Value.Month == filter.Month.Value)
                ).ToList();

                // Tạo một đối tượng EmployeeMysqlDto mới và cập nhật TotalVacationsCount
                return new EmployeeMysqlDto
                {
                    // Sao chép/Ánh xạ các thuộc tính của EmployeeMysqlDto
                    EmployeeNumber = e.EmployeeNumber,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    PaidToDate = e.PaidToDate,
                    PayRatesIdPayRates = e.PayRatesIdPayRates,
                    Vacations = filteredVacations,
                    TotalVacationsCount = filteredVacations.Count, // Cập nhật số lượng Vacations phù hợp
                };
            })
            .Where(employee => employee.Vacations.Any())
            .ToList();

            return filteredData;
        }

        public async Task<IEnumerable<Employee?>> GetByAllEmployee()
        {
            return await mysqlContext.Employees.ToListAsync();
        }

    }
}
