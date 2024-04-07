using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class SqlDataRepository : ISqlDataRepository
    {
        private readonly SqlServerContext? sqlServerContext;
        public SqlDataRepository(SqlServerContext? sqlServerContext)
        {
            this.sqlServerContext = sqlServerContext;
        }

        // lấy ra danh sách benefit
        public async Task<IEnumerable<BenefitPlan?>> GetByAll()
        {
            return await sqlServerContext.BenefitPlans.ToListAsync();
        }

        public async Task<BenefitPlan?> GetBenefitPlanById(uint benefit)
        {
            return await sqlServerContext.BenefitPlans.FirstOrDefaultAsync(x => x.BenefitPlanId == (decimal)benefit);
        }

        // lấy dữ liệu từ sql server
        public async Task<List<EmployeeSqlServerDto>> FetchSqlServerData()
        {
            /*var query =*/
            return await sqlServerContext.Personals
                .Select(e => new EmployeeSqlServerDto
                {
                    EmployeeId = Convert.ToUInt32(e.EmployeeId),
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    ShareholderStatus = e.ShareholderStatus,
                    Gender = e.Gender,
                    Ethnicity = e.Ethnicity,
                    StartDate = e.JobHistories.Where(jb => jb.EmployeeId == e.EmployeeId).Select(jb => jb.StartDate).FirstOrDefault(),
                    EndDate = e.JobHistories.Where(jb => jb.EmployeeId == e.EmployeeId).Select(jb => jb.EndDate).FirstOrDefault(),
                    Employments = e.Employment,
                    JobHistories = e.JobHistories.ToList(),
                }).ToListAsync();
        }

        // lọc lịch sử làm việc của nhân viên dựa vào endate
        public List<EmployeeSqlServerDto> FilterJobHistorySqlServe(List<EmployeeSqlServerDto> data, EmployeeFilterDto filter)
        {
            var query = data.Where(p => p.JobHistories.Any(jb =>
                jb.EndDate.HasValue &&
               ((!filter.Year.HasValue || jb.EndDate.Value.Year == filter.Year.Value) && // Kiểm tra năm nếu có
               (!filter.Month.HasValue || jb.EndDate.Value.Month == filter.Month.Value)))) // Kiểm tra tháng nếu có
               .ToList();
            return query;
        }
    }
}
