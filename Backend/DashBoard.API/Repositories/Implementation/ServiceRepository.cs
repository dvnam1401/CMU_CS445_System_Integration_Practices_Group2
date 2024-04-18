using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Models.Inteface;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.ComponentModel;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DashBoard.API.Repositories.Implementation
{
    public class ServiceRepository : IServiceRepository
    {

        private readonly IMysqlDataRepository mysqlDataRepository;
        private readonly ISqlDataRepository sqlDataRepository;

        public ServiceRepository(IMysqlDataRepository mysqlDataRepository, ISqlDataRepository sqlDataRepository)
        {

            this.mysqlDataRepository = mysqlDataRepository;
            this.sqlDataRepository = sqlDataRepository;
        }

        public Task<BenefitPlan?> GetBenefitPlanById(decimal BenefitPlansId)
        {
            return sqlDataRepository.GetBenefitPlanById(BenefitPlansId);
        }

        public Task<IEnumerable<BenefitPlan?>> GetByAll()
        {
            return sqlDataRepository.GetByAll();
        }

        public Task<IEnumerable<Employee?>> GetByAllEmployee()
        {
            return mysqlDataRepository.GetByAllEmployee();
        }

        // lọc lịch sử làm việc của nhân viên dựa vào endate
        //private List<EmploymentSqlServerDto> FilterJobHistorySqlServe(List<EmploymentSqlServerDto> data, EmployeeFilterDto filter)
        //{
        //    var query = data.Where(p => p.JobHistories.Any(jb =>
        //        jb.EndDate.HasValue &&
        //       ((!filter.Year.HasValue || jb.EndDate.Value.Year == filter.Year.Value) && // Kiểm tra năm nếu có
        //       (!filter.Month.HasValue || jb.EndDate.Value.Month == filter.Month.Value)))) // Kiểm tra tháng nếu có
        //       .ToList();
        //    return query;
        //}

        // lọc nhân viên theo giới tính, chức vụ,...
        //private IEnumerable<T> FilterEmployees<T>(IEnumerable<T> employees, EmployeeFilterDto filter) where T : IEmployeeData
        //{
        //    return employees
        //        .Where(e => !filter.Gender || e.Gender == filter.Gender)
        //        .Where(e => string.IsNullOrEmpty(filter.Gender) || filter.Gender.ToLower() == "null" || (e.Gender != null && e.Gender.Equals(filter.Gender, StringComparison.OrdinalIgnoreCase)))
        //        .Where(e => string.IsNullOrEmpty(filter.Ethnicity) || filter.Ethnicity.ToLower() == "null" || (e.Ethnicity != null && e.Ethnicity.Equals(filter.Ethnicity, StringComparison.OrdinalIgnoreCase)))
        //        .Where(e => string.IsNullOrEmpty(filter.Department) || filter.Department.ToLower() == "null" || e.JobHistories.Any(jh => jh.Department == filter.Department))
        //        .Where(e => string.IsNullOrEmpty(filter.Category) || filter.Category.ToLower() == "null" || (e.Category != null && e.Category.Equals(filter.Category, StringComparison.OrdinalIgnoreCase)));
        //}

        //private IEnumerable<T> ApplySorting<T>(IEnumerable<T> filteredResult, bool? isAscending) where T : IEmployeeData
        //{
        //    return isAscending.HasValue
        //        ? isAscending == true
        //            ? filteredResult.OrderBy(e => e.FullName)
        //            : filteredResult.OrderByDescending(e => e.FullName)
        //        : filteredResult;
        //}
        private IEnumerable<T> FilterEmployees<T>(IEnumerable<T> employees, EmployeeFilterDto filter) where T : class, IEmployeeData
        {
            // Apply filters only if a value is provided
            if (!string.IsNullOrEmpty(filter.Gender) && filter.Gender.ToLower() != "null")
            {
                employees = employees.Where(e => e.Gender != null && e.Gender.Equals(filter.Gender, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filter.Ethnicity) && filter.Ethnicity.ToLower() != "null")
            {
                employees = employees.Where(e => e.Ethnicity != null && e.Ethnicity.Equals(filter.Ethnicity, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filter.Department) && filter.Department.ToLower() != "null")
            {
                employees = employees.Where(e => e.JobHistories.Any(jh => jh.Department != null && jh.Department.Equals(filter.Department, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrEmpty(filter.Category) && filter.Category.ToLower() != "null")
            {
                employees = employees.Where(e => e.Category != null && e.Category.Equals(filter.Category, StringComparison.OrdinalIgnoreCase));
            }

            // Return the filtered queryable
            return employees;
        }

        private IEnumerable<T> ApplySorting<T>(IEnumerable<T> filteredResult, bool? isAscending) where T : class, IEmployeeData
        {
            // Apply sorting based on the isAscending flag
            if (isAscending.HasValue)
            {
                filteredResult = isAscending.Value
                    ? filteredResult.OrderBy(e => e.FullName)
                    : filteredResult.OrderByDescending(e => e.FullName);
            }

            return filteredResult;
        }

        // Tổng số ngày nghỉ phép của nhân viên
        public async Task<IEnumerable<NumberOfVacationDay>> GetNumberOfVacationDays(EmployeeFilterDto filter)
        {
            // Khởi tạo tác vụ để lấy dữ liệu nhân viên và kỳ nghỉ
            var employeeTask = mysqlDataRepository.FetchMysqlEmployeeDataAsync();
            var dataSqlServerTask = sqlDataRepository.FetchSqlServerData(filter);
            // Lấy kết quả từ các tác vụ
            var dataMysql = await employeeTask;
            var dataSqlServer = await dataSqlServerTask;

            // Tiếp tục xử lý với dữ liệu đã được tải
            var query = JoinAndTransformData(dataMysql, dataSqlServer);
            var filteredQuery = FilterEmployees<NumberOfVacationDay>(query, filter);
            var sortedQuery = ApplySorting<NumberOfVacationDay>(filteredQuery, filter.IsAscending);
            return sortedQuery;
        }


        //var vacations = await vacationTask;
        //await Task.WhenAll(employeeTask, vacationTask);


        // Gọi phương thức kết hợp dữ liệu và tiếp tục xử lý

        // Khi đến đây, cả hai tác vụ đều đã hoàn thành
        ////dataMysql = mysqlDataRepository.FilterEmployeeDataByVacation(dataMysql, filter);

        //var dataMysql = employees;//mysqlDataRepository.FeatchMysql(employees/*, vacations*/);

        //if (filter.Year.HasValue || filter.Month.HasValue)
        //{
        //    dataMysql = mysqlDataRepository.FilterEmployeeDataByVacation(dataMysql, filter);
        //}

        //JoinAndTransformData
        private IEnumerable<NumberOfVacationDay> JoinAndTransformData(IEnumerable<EmployeeMysqlDto> dataMysql, IEnumerable<EmploymentSqlServerDto> dataSqlServer)
        {
            return from e in dataMysql
                   join p in dataSqlServer on e.EmployeeId equals p.EmploymentId
                   select new NumberOfVacationDay
                   {
                       ShareholderStatus = p.ShareholderStatus,
                       FullName = $"{e.LastName} {e.FirstName}",
                       Gender = p.Gender,
                       Ethnicity = p.Ethnicity?.Trim(),
                       //Category = p.JobHistories?.FirstOrDefault()?.JobTitle,
                       Category = p.EmploymentStatus?.ToString().Trim(),
                       JobHistories = p.JobHistories != null ? new List<JobHistoryDto>(p.JobHistories) : new List<JobHistoryDto>(),
                       TotalDaysOff = p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth,
                   };
        }
        public async Task<IEnumerable<EmployeeSalaryDto>> GetEmployeesSalary(EmployeeFilterDto filter)
        {
            // Khởi tạo tác vụ để lấy dữ liệu nhân viên và kỳ nghỉ
            var employeeTask = mysqlDataRepository.FetchMysqlEmployeeDataAsync();
            var dataSqlServerTask = sqlDataRepository.FetchSqlServerData(filter);
            await Task.WhenAll(employeeTask, dataSqlServerTask);
            // Lấy kết quả từ các tác vụ
            var dataMysql = await employeeTask;
            var dataSqlServer = await dataSqlServerTask;

            // Tiếp tục xử lý với dữ liệu đã được tải
            var query = JoinAndTransformDataSalary(dataMysql, dataSqlServer);
            var filteredQuery = FilterEmployees<EmployeeSalaryDto>(query, filter);
            var sortedQuery = ApplySorting<EmployeeSalaryDto>(filteredQuery, filter.IsAscending);

            return sortedQuery;
            //// Tiếp tục xử lý với dữ liệu đã được tải
            //var employeeResult = JoinAndTransformDataSalary(dataMysql, dataSqlServer);
            //var filteredResult = FilterEmployees<EmployeeSalaryDto>(employeeResult, filter);
            //return ApplySorting<EmployeeSalaryDto>(filteredResult, filter.IsAscending);
        }

        private IEnumerable<EmployeeSalaryDto> JoinAndTransformDataSalary(IEnumerable<EmployeeMysqlDto> dataMysql, IEnumerable<EmploymentSqlServerDto> dataSqlServer)
        {
            return from e in dataMysql
                   join p in dataSqlServer on e.EmployeeId equals p.EmploymentId
                   select new EmployeeSalaryDto
                   {
                       ShareholderStatus = p.ShareholderStatus,
                       FullName = $"{e.LastName} {e.FirstName}",
                       Gender = p.Gender,
                       Ethnicity = p.Ethnicity?.Trim(),
                       Category = p.JobHistories?.FirstOrDefault()?.JobTitle,
                       JobHistories = p.JobHistories != null ? new List<JobHistoryDto>(p.JobHistories) : new List<JobHistoryDto>(),
                       //TotalSalary = p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth,
                       TotalSalary = (e.PayRate * p.NumberDaysRequirementOfWorkingPerMonth +
                                    e.PayRate * p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth) -
                                    (e.PayRate * p.NumberDaysRequirementOfWorkingPerMonth +
                                   (e.PayRate * p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth) *
                                    e.PayRatesIdPayRates.TaxPercentage) / 100,
                   };
        }

        public Task<IEnumerable<EmployeeAnniversaryDto>> GetEmployeesAnniversaryInfo(int daysLimit)
        {
            throw new NotImplementedException();
        }

        //// lấy ra số ngày kỉ niệm của nhân viên
        //public async Task<IEnumerable<EmployeeAnniversaryDto>> GetEmployeesAnniversaryInfo(int daysLimit)
        //{
        //    var today = DateTime.Today;
        //    var personal = await sqlDataRepository.FetchSqlServerData();

        //    // Trước tiên, lọc danh sách để chỉ giữ lại những đối tượng có HireDate và số ngày đến ngày kỷ niệm nằm trong phạm vi được truyền vào
        //    var employeeAnniversary = personal
        //        .Where(e => e.Employments?.HireDate is not null)
        //        .Select(e =>
        //        {
        //            // Tính ngày kỷ niệm trong năm hiện tại
        //            var hireDateThisYear = new DateTime(today.Year, e.Employments.HireDate.Value.Month, e.Employments.HireDate.Value.Day);

        //            // Nếu ngày kỷ niệm đã qua, chuyển sang năm tiếp theo
        //            if (hireDateThisYear < today)
        //            {
        //                hireDateThisYear = hireDateThisYear.AddYears(1);
        //            }
        //            var daysUntilNextAnniversary = (hireDateThisYear - today).Days;

        //            // Kiểm tra số ngày còn lại có trong phạm vi được yêu cầu không
        //            if (daysUntilNextAnniversary > daysLimit)
        //            {
        //                return null; // Loại bỏ những nhân viên không trong phạm vi
        //            }
        //            var content = $"Số ngày còn lại trước khi tới ngày kỷ niệm: {daysUntilNextAnniversary}";
        //            var department = e.JobHistories?.FirstOrDefault()?.Department ?? "Không rõ";

        //            return new EmployeeAnniversaryDto
        //            {
        //                FullName = $"{e.LastName} {e.FirstName}",
        //                Department = department,
        //                Content = content
        //            };
        //        }).Where(e => e is not null);

        //    //{
        //    //    if (e.Employments?.HireDate is null) return false;
        //    //    var hireDateThisYear = new DateTime(today.Year, e.Employments.HireDate.Value.Month, e.Employments.HireDate.Value.Day);
        //    //    if (hireDateThisYear < today) hireDateThisYear = hireDateThisYear.AddYears(1); // Chuyển sang năm tiếp theo nếu đã qua
        //    //    var daysUntilNextAnniversary = (hireDateThisYear - today).Days;
        //    //    return daysUntilNextAnniversary <= daysLimit;
        //    //});

        //    //// Sau đó, chuyển đổi những đối tượng đã lọc thành định dạng đầu ra mong muốn
        //    //var employeeAnniversaryInfos = filteredPersonal.Select(e =>
        //    //{
        //    //    var nextAnniversary = new DateTime(today.Year, e.Employments.HireDate.Value.Month, e.Employments.HireDate.Value.Day);
        //    //    if (nextAnniversary < today) nextAnniversary = nextAnniversary.AddYears(1);
        //    //    var daysUntilNextAnniversary = (nextAnniversary - today).Days;

        //    //    var content = $"Số ngày còn lại trước khi tới ngày kỷ niệm: {daysUntilNextAnniversary}";
        //    //    var department = e.JobHistories?.FirstOrDefault()?.Department ?? "Không rõ";

        //    //    return new EmployeeAnniversaryDto
        //    //    {
        //    //        FullName = $"{e.LastName} {e.FirstName}",
        //    //        Department = department,
        //    //        Content = content
        //    //    };
        //    //});

        //    return employeeAnniversary.ToList(); // Chuyển IEnumerable thành List nếu bạn muốn kết quả là một danh sách cụ thể
        //}

        //public async Task<IEnumerable<EmployeeVacationDto>> GetEmployeesWithAccumulatedVacationDays(int minimumDays)
        //{
        //    // Khởi tạo tác vụ để lấy dữ liệu nhân viên và kỳ nghỉ
        //    var employeeTask = mysqlDataRepository.FetchMysqlEmployeeDataAsync();
        //    var vacationTask = mysqlDataRepository.FeatchMysqlDetailVacationAsync();

        //    // Đợi cho cả hai tác vụ hoàn thành
        //    await Task.WhenAll(employeeTask, vacationTask);
        //    //Console.WriteLine(minimumDays);
        //    // Lấy kết quả từ các tác vụ
        //    var employees = await employeeTask;
        //    var vacations = await vacationTask;
        //    //var filterYear = DateTime.Today.Year;
        //    EmployeeFilterDto filter = new EmployeeFilterDto
        //    {
        //        Year = DateTime.Today.Year,
        //        Month = null,
        //    };

        //    var dataMysql = mysqlDataRepository.FeatchMysql(employees, vacations);
        //    dataMysql = mysqlDataRepository.FilterEmployeeDataByVacation(dataMysql,
        //                                                                 filter);

        //    var employeesWithAccumulatedDays = dataMysql
        //        .Where(e => e.TotalVacationsCount > minimumDays)                
        //        .Select(e => new EmployeeVacationDto
        //        {                    
        //            FullName = $"{e.FirstName} {e.LastName}",
        //            Department = "",
        //            Content = $"Đã nghỉ {e.TotalVacationsCount} ngày, quá số ngày nghỉ phép quy định ",
        //            AccumulatedVacationDays = e.TotalVacationsCount,
        //        })
        //        .ToList();

        //    return employeesWithAccumulatedDays;
        //}

        //private IEnumerable<EmployeeBirthdayDto> JoinAndTransformDataBirthday(IEnumerable<EmployeeMysqlDto> dataMysql, IEnumerable<EmployeeSqlServerDto> dataSqlServer)
        //{
        //    return from e in dataMysql
        //           join p in dataSqlServer on e.EmployeeNumber equals p.EmployeeId
        //           select new EmployeeBirthdayDto
        //           {
        //               FullName = $"{e.LastName} {e.FirstName}",
        //               Department = p.JobHistories?.FirstOrDefault()?.Department ?? "Không rõ",
        //               BirthDay = e.BirthDate?.Dateofbirthday
        //           };
        //}


        //public async Task<IEnumerable<EmployeeBirthdayDto>> GetEmployeesWithBirthdaysThisMonth()
        //{
        //    // Khởi tạo tác vụ để lấy dữ liệu nhân viên và kỳ nghỉ
        //    var employeeTask = mysqlDataRepository.FetchMysqlEmployeeDataAsync();
        //    var vacationTask = mysqlDataRepository.FeatchMysqlDetailVacationAsync();

        //    // Đợi cho cả hai tác vụ hoàn thành
        //    await Task.WhenAll(employeeTask, vacationTask);

        //    // Lấy kết quả từ các tác vụ
        //    var employees = await employeeTask;
        //    var vacations = await vacationTask;

        //    // Khi đến đây, cả hai tác vụ đều đã hoàn thành
        //    var dataMysql = mysqlDataRepository.FeatchMysql(employees, vacations);

        //    var dataSqlServerTask = sqlDataRepository.FetchSqlServerData();
        //    var dataSqlServer = await dataSqlServerTask;


        //    // Tiếp tục xử lý với dữ liệu đã được tải
        //    var employeeResult = JoinAndTransformDataBirthday(dataMysql, dataSqlServer);
        //    var today = DateTime.Today;
        //    var employeesWithBirthdays = employeeResult
        //        .Where(e => e.BirthDay is not null)
        //        .Where(e => e.BirthDay.Value.Month == today.Month)
        //        .Select(e =>
        //        {
        //            var birthdayThisYear = new DateTime(today.Year, e.BirthDay.Value.Month, e.BirthDay.Value.Day);
        //            if (birthdayThisYear < today)
        //            {
        //                birthdayThisYear = birthdayThisYear.AddYears(1);
        //            }

        //            var countBirthday = (birthdayThisYear - today).Days;
        //            var content = $"Còn {countBirthday} là tới ngày sinh nhật";
        //            var department = e.Department;
        //            return new EmployeeBirthdayDto
        //            {
        //                FullName = e.FullName,
        //                Department = department,
        //                Content = content
        //            };
        //        }).ToList();
        //    return employeesWithBirthdays;
        //}
    }
}
