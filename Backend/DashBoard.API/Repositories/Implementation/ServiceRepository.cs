using DashBoard.API.Data;
using DashBoard.API.Models;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DashBoard.API.Repositories.Implementation
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly SqlServerContext? sqlServerContext;

        private readonly MysqlContext? mysqlContext;

        public ServiceRepository(SqlServerContext? sqlServerContext, MysqlContext? mysqlContext)
        {
            this.sqlServerContext = sqlServerContext;
            this.mysqlContext = mysqlContext;
        }

        public async Task<IEnumerable<BenefitPlan?>> GetByAll()
        {
            return await sqlServerContext.BenefitPlans.ToListAsync();
        }

        public async Task<BenefitPlan?> GetById(int benefit)
        {
            return await sqlServerContext.BenefitPlans.FirstOrDefaultAsync(x => x.BenefitPlanId == benefit);
        }

        public async Task<IEnumerable<Employee?>> GetByAllEmployee()
        {
            return await mysqlContext.Employees.ToListAsync();
        }
        public async Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeAllSalary()
        {
            var dataMysql = mysqlContext.Employees
                .Select(e => new
                {
                    e.EmployeeNumber,
                    e.LastName,
                    e.FirstName,
                    e.PayRatesIdPayRatesNavigation,
                }).ToList();
            var dataSqlServer = sqlServerContext.Personals
                .Select(e => new
                {
                    EmployeeId = Convert.ToInt32(e.EmployeeId),
                    e.ShareholderStatus,
                    e.Gender,
                    e.Ethnicity,
                }).ToList();
            var employeeResult = from e in dataMysql
                                 join p in dataSqlServer on e.EmployeeNumber equals p.EmployeeId
                                 select new EmployeeSalaryDto
                                 {
                                     ShareholderStatus = p.ShareholderStatus,
                                     FullName = $"{e.LastName} {e.FirstName}",
                                     Gender = p.Gender.HasValue,
                                     Ethnicity = p.Ethnicity,
                                     PayRateName = e.PayRatesIdPayRatesNavigation.PayRateName,
                                     TotalIncome = 1,
                                 };
            return employeeResult;
        }

        public async Task<IEnumerable<EmployeeSalaryDto>> GetEmployeesByFilter(EmployeeFilterDto filter)
        {
            var dataMysql = mysqlContext.Employees
                .Select(e => new
                {
                    e.EmployeeNumber,
                    e.LastName,
                    e.FirstName,
                    e.PayRatesIdPayRatesNavigation,
                }).ToList();
            var dataSqlServer = sqlServerContext.Personals
                .Select(e => new
                {
                    EmployeeId = Convert.ToInt32(e.EmployeeId),
                    e.ShareholderStatus,
                    e.Gender,
                    e.Ethnicity,
                    e.JobHistories,
                }).ToList();
            var employeeResult = from e in dataMysql
                                 join p in dataSqlServer on e.EmployeeNumber equals p.EmployeeId
                                 select new EmployeeSalaryDto
                                 {
                                     ShareholderStatus = p.ShareholderStatus,
                                     FullName = $"{e.LastName} {e.FirstName}",
                                     Gender = p.Gender.HasValue,
                                     Ethnicity = p.Ethnicity,
                                     PayRateName = e.PayRatesIdPayRatesNavigation.PayRateName,
                                     JobHistories = new List<JobHistory>(p.JobHistories),
                                     TotalIncome = 1,
                                 };
            //var filteredResult = employeeResult
            //    .Where(e => !filter.Gender.HasValue || e.Gender == filter.Gender)
            //    .Where(e => string.IsNullOrEmpty(filter.Ethnicity) || (e.Ethnicity != null && e.Ethnicity.Contains(filter.Ethnicity)))
            //    .Where(e => string.IsNullOrEmpty(filter.Department) || e.JobHistories.Any(jh => jh.Department == filter.Department))
            //    .Where(e => string.IsNullOrEmpty(filter.PayRateName) || e.PayRateName.Contains(filter.PayRateName)).ToList();
            var filteredResult = employeeResult
                .Where(e => !filter.Gender.HasValue || e.Gender == filter.Gender)
                .Where(e => string.IsNullOrEmpty(filter.Ethnicity) || filter.Ethnicity.ToLower() == "null" || (e.Ethnicity != null && e.Ethnicity.Contains(filter.Ethnicity)))
                .Where(e => string.IsNullOrEmpty(filter.Department) || filter.Department.ToLower() == "null" || e.JobHistories.Any(jh => jh.Department == filter.Department))
                .Where(e => string.IsNullOrEmpty(filter.PayRateName) || filter.PayRateName.ToLower() == "null" || e.PayRateName.Contains(filter.PayRateName));

            if (filter.IsAscending is not null)
            {
                IEnumerable<EmployeeSalaryDto> sortedFilteredResult;
                sortedFilteredResult = (bool)filter.IsAscending ?
                    filteredResult.OrderBy(e => e.FullName) :
                    filteredResult.OrderByDescending(e => e.FullName);
                return sortedFilteredResult;
            }
            return filteredResult;
        }
        public async Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeSalaryByGender(bool Gender)
        {
            var dataMysql = mysqlContext.Employees
                .Select(e => new
                {
                    e.EmployeeNumber,
                    e.LastName,
                    e.FirstName,
                    e.PayRatesIdPayRatesNavigation,
                }).ToList();
            var dataSqlServer = sqlServerContext.Personals
                .Where(p => p.Gender == Gender)
                .Select(e => new
                {
                    EmployeeId = Convert.ToInt32(e.EmployeeId),
                    e.ShareholderStatus,
                    e.Gender,
                    e.Ethnicity,
                }).ToList();
            var employeeResult = from e in dataMysql
                                 join p in dataSqlServer on e.EmployeeNumber equals p.EmployeeId
                                 select new EmployeeSalaryDto
                                 {
                                     ShareholderStatus = p.ShareholderStatus,
                                     FullName = $"{e.LastName} {e.FirstName}",
                                     //Gender = p.Gender,
                                     Ethnicity = p.Ethnicity,
                                     PayRateName = e.PayRatesIdPayRatesNavigation.PayRateName,
                                     TotalIncome = 0,
                                 };
            return employeeResult;
        }
        //public async Task<List<EmployeeSalaryDto>> GetEmployeesByFilter(EmployeeFilterDto filter)
        //{
        //    var queryMysql = mysqlContext.Employees.AsQueryable();
        //    var querySql = sqlServerContext.Personals.AsQueryable();

        //    // Giả định có trường ShareholderStatus trong đối tượng Employee
        //    if (filter.ShareholderStatus.HasValue)
        //    {
        //        querySql = querySql.Where(x => x.ShareholderStatus == filter.ShareholderStatus);
        //    }

        //    // Sắp xếp
        //    if (!string.IsNullOrEmpty(filter.FullName))
        //    {
        //        // Sắp xếp tăng dần theo FullName
        //        if (filter.SortOrder.ToLower() == "asc")
        //        {
        //            querySql = querySql.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
        //        }
        //        // Sắp xếp giảm dần theo FullName
        //        else if (filter.SortOrder.ToLower() == "desc")
        //        {
        //            querySql = querySql.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);
        //        }
        //    }

        //    // Giả định có trường Gender dưới dạng bool trong đối tượng Employee
        //    if (filter.Gender)
        //    {
        //        querySql = querySql.Where(x => x.Gender != null && x.Gender == filter.Gender);
        //    }

        //    if (!string.IsNullOrEmpty(filter.PayRateName))
        //    {
        //        queryMysql = queryMysql.Where(x => x.PayRatesIdPayRatesNavigation.PayRateName == filter.PayRateName);
        //    }

        //    // Xử lý cho Ethnicity và Department (điều chỉnh lại nếu có sai sót do không thấy rõ định nghĩa của model)
        //    if (!string.IsNullOrEmpty(filter.Ethnicity))
        //    {
        //        // Giả định có trường Ethnicity trong đối tượng Employee
        //        querySql = querySql.Where(x => x.Ethnicity == filter.Ethnicity);
        //    }

        //    if (!string.IsNullOrEmpty(filter.Department))
        //    {
        //        // Giả định có trường Department trong đối tượng Employee
        //        querySql = querySql.Where(x => x.JobHistories.Any(j => j.Department == filter.Department));
        //    }

        //    var mysqlEmployees = await queryMysql.Select(x => new
        //    {
        //        x.EmployeeNumber,
        //        x.LastName,
        //        x.FirstName,
        //        x.PayRatesIdPayRatesNavigation.PayRateName,
        //    }).ToListAsync();

        //    var sqlServerPersonals = await querySql.Select(x => new
        //    {
        //        x.EmployeeId,
        //        x.ShareholderStatus,
        //        x.Gender,
        //        x.Ethnicity,
        //    }).ToListAsync();


        //    var employees = mysqlEmployees.Select(e => new EmployeeSalaryDto
        //    {
        //        FullName = $"{e.LastName} {e.FirstName}",
        //        PayRateName = e.PayRateName,
        //        ShareholderStatus = sqlServerPersonals.FirstOrDefault(p => p.EmployeeId == e.EmployeeNumber)?.ShareholderStatus ?? false,
        //        Gender = sqlServerPersonals.FirstOrDefault(p => p.EmployeeId == e.EmployeeNumber)?.Gender ?? false,
        //        Ethnicity = sqlServerPersonals.FirstOrDefault(p => p.EmployeeId == e.EmployeeNumber)?.Ethnicity
        //    }).ToList();

        //    return employees;
        //}

        public Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeSalaryEthnicity(string ethnicity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeSalaryByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeSalaryDto>> GetEmployeeSalaryByDepartment(string department)
        {
            //var contextMysql = from e in dbMysqlContext.Employees
            //                   join p in dbMysqlContext.Payrates on
            throw new NotImplementedException();
        }
    }
}
