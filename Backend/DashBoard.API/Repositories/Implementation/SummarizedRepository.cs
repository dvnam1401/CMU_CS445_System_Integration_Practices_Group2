using DashBoard.API.Models.DTO;
using DashBoard.API.Models.Inteface;
using DashBoard.API.Repositories.Inteface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoard.API.Repositories.Implementation
{
    public class SummarizedRepository : ISummarizedRepository
    {
        private readonly IMysqlDataRepository mysqlDataRepository;
        private readonly ISqlDataRepository sqlDataRepository;

        public SummarizedRepository(IMysqlDataRepository mysqlDataRepository, ISqlDataRepository sqlDataRepository)
        {

            this.mysqlDataRepository = mysqlDataRepository;
            this.sqlDataRepository = sqlDataRepository;
        }

        public List<string> GetAllDepartments()
        {
            return sqlDataRepository.GetDepartments();
        }

        public async Task<IEnumerable<EmployeeAverageBenefitDto>> GetEmployeeAverageBenefit(EmployeeFilterDto filter)
        {
            // Khởi tạo tác vụ để lấy dữ liệu nhân viên và kỳ nghỉ
            var employeeTask = mysqlDataRepository.FetchMysqlEmployeeDataAsync();
            var dataSqlServerTask = sqlDataRepository.FetchSqlServerData(filter);
            await Task.WhenAll(employeeTask, dataSqlServerTask);
            // Lấy kết quả từ các tác vụ
            var dataMysql = await employeeTask;
            var dataSqlServer = await dataSqlServerTask;

            // Tiếp tục xử lý với dữ liệu đã được tải
            var query = JoinAndTransformDataAverageBenefit(dataMysql, dataSqlServer);
            var filteredQuery = FilterEmployees<EmployeeAverageBenefitDto>(query, filter);
            var sortedQuery = ApplySorting<EmployeeAverageBenefitDto>(filteredQuery, filter.IsAscending);

            return sortedQuery;
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
        }

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

        private IEnumerable<EmployeeAverageBenefitDto> JoinAndTransformDataAverageBenefit(IEnumerable<EmployeeMysqlDto> dataMysql, IEnumerable<EmploymentSqlServerDto> dataSqlServer)
        {
            return from e in dataMysql
                   join p in dataSqlServer on e.EmployeeId equals p.EmploymentId
                   select new EmployeeAverageBenefitDto
                   {
                       ShareholderStatus = p.ShareholderStatus,
                       FullName = $"{p.FirstName} {p.LastName} {p.MiddleName}",
                       Gender = p.Gender.ToUpper(),
                       Ethnicity = p.Ethnicity?.Trim(),
                       Category = p.JobHistories?.FirstOrDefault()?.JobTitle,
                       JobHistories = p.JobHistories != null ? new List<JobHistoryDto>(p.JobHistories) : new List<JobHistoryDto>(),
                       //TotalSalary = p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth,
                       TotalAverageBenefit = p.BenefitPlan,
                   };
        }

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

        private IEnumerable<EmployeeSalaryDto> JoinAndTransformDataSalary(IEnumerable<EmployeeMysqlDto> dataMysql, IEnumerable<EmploymentSqlServerDto> dataSqlServer)
        {
            var result = from e in dataMysql
                         join p in dataSqlServer on e.EmployeeId equals p.EmploymentId
                         select new EmployeeSalaryDto
                         {
                             ShareholderStatus = p.ShareholderStatus,
                             FullName = $"{p.FirstName} {p.LastName} {p.MiddleName}",
                             Gender = p.Gender,
                             Ethnicity = p.Ethnicity?.Trim(),
                             Category = p.JobHistories?.FirstOrDefault()?.JobTitle,
                             JobHistories = p.JobHistories != null ? new List<JobHistoryDto>(p.JobHistories) : new List<JobHistoryDto>(),
                             TotalSalary = e.PayRatesIdPayRates.Value,
                             //TotalSalary = p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth,
                             //TotalSalary = (e.PayRate * p.NumberDaysRequirementOfWorkingPerMonth +
                             //             e.PayRate * p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth) -
                             //             (e.PayRate * p.NumberDaysRequirementOfWorkingPerMonth +
                             //            (e.PayRate * p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth) *
                             //             e.PayRatesIdPayRates.TaxPercentage) / 100,
                         };
            return result;
        }

        private IEnumerable<NumberOfVacationDay> JoinAndTransformData(IEnumerable<EmployeeMysqlDto> dataMysql, IEnumerable<EmploymentSqlServerDto> dataSqlServer)
        {
            var result = from e in dataMysql
                         join p in dataSqlServer on e.EmployeeId equals p.EmploymentId
                         select new NumberOfVacationDay
                         {
                             ShareholderStatus = p.ShareholderStatus,
                             FullName = $"{p.FirstName} {p.MiddleName} {p.LastName} ",
                             Gender = p.Gender,
                             Ethnicity = p.Ethnicity?.Trim(),
                             //Category = p.JobHistories?.FirstOrDefault()?.JobTitle,
                             Category = p.EmploymentStatus?.ToString().Trim(),
                             JobHistories = p.JobHistories != null ? new List<JobHistoryDto>(p.JobHistories) : new List<JobHistoryDto>(),
                             //TotalDaysOff = p.WorkingTime?.SingleOrDefault(wk => wk.TotalNumberVacationWorkingDaysPerMonth != null)?.TotalNumberVacationWorkingDaysPerMonth,
                             TotalDaysOff = p.WorkingTime?.Sum(wk => wk.TotalNumberVacationWorkingDaysPerMonth ?? 0),
                         };
            return result;
        }
    }
}
