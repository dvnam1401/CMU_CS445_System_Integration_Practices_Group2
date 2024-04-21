using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
        public async Task<IEnumerable<BenefitPlan?>> GetByAllBenefitPlan()
        {
            return await sqlServerContext.BenefitPlans.ToListAsync();
        }

        public async Task<IEnumerable<EmploymentDto?>> FetchEmployments()
        {
            using (var sqlServerContext = new SqlServerContext()) // Tạo phiên bản DbContext mới
            {
                var query = sqlServerContext.Employments.AsQueryable();
                // Project the filtered job histories to JobHistoryDto
                var result = await query.Select(e => new EmploymentDto
                {
                    EmploymentId = e.EmploymentId,
                    LastName = e.Personal.CurrentLastName,
                    FirstName = e.Personal.CurrentFirstName,
                    Birthday = e.Personal.BirthDate,
                    Department = e.JobHistories.FirstOrDefault().Department,
                    HireDateForWorking = e.HireDateForWorking,
                    RehireDateForWorking = e.RehireDateForWorking,
                }).ToListAsync();

                return result;
            }
        }

        public async Task<BenefitPlan?> GetBenefitPlanById(decimal BenefitPlansId)
        {
            return await sqlServerContext.BenefitPlans.FirstOrDefaultAsync(x => x.BenefitPlansId == BenefitPlansId);
        }

        // lấy số ngày nghỉ dựa vào filter
        public async Task<IEnumerable<EmploymentWorkingTimeDto>> FetchWorkingTimes(EmployeeFilterDto? filter)
        {
            using (var sqlServerContext = new SqlServerContext()) // Tạo phiên bản DbContext mới
            {
                var query = sqlServerContext.EmergencyContacts.AsQueryable();
                if (filter != null)
                {
                    if (filter.Year.HasValue)
                    {
                        query = query.Where(ewt => ewt.YearWorking.HasValue && ewt.YearWorking.Value.Year == filter.Year);
                    }
                    if (filter.Month.HasValue)
                    {
                        query = query.Where(ewt => ewt.MonthWorking.HasValue && ewt.MonthWorking.Value == filter.Month);
                    }
                }
                var result = await query.Select(ewt => new EmploymentWorkingTimeDto
                {
                    EmploymentWorkingTimeId = ewt.EmploymentWorkingTimeId,
                    EmploymentId = ewt.EmploymentId,
                    YearWorking = ewt.YearWorking,
                    MonthWorking = ewt.MonthWorking,
                    NumberDaysActualOfWorkingPerMonth = ewt.NumberDaysActualOfWorkingPerMonth,
                    TotalNumberVacationWorkingDaysPerMonth = ewt.TotalNumberVacationWorkingDaysPerMonth
                }).ToListAsync();
                //var filteredData = await result.ToListAsync();
                return result;
            }
        }

        // lấy tổng số ngày nghỉ 1 năm
        public async Task<IEnumerable<EmploymentWorkingTimeDto>> FetchWorkingTimes(int minimumDays)
        {
            using (var sqlServerContext = new SqlServerContext()) // Tạo phiên bản DbContext mới
            {
                var query = sqlServerContext.EmergencyContacts.AsQueryable();
                var currentYear = DateTime.Now.Year;
                //var currentYear = 1996;
                var result = await query
                    .Where(ewt => ewt.YearWorking.Value.Year == currentYear)
                    .GroupBy(ewt => new { ewt.EmploymentId, ewt.YearWorking })
                    .Select(group => new EmploymentWorkingTimeDto
                    {
                        EmploymentWorkingTimeId = group.First().EmploymentWorkingTimeId,
                        EmploymentId = group.Key.EmploymentId,
                        YearWorking = group.Key.YearWorking,
                        TotalNumberVacationWorkingDaysPerMonth = group.Sum(x => x.TotalNumberVacationWorkingDaysPerMonth)
                    }).Where(x => x.TotalNumberVacationWorkingDaysPerMonth > minimumDays)
                    .ToListAsync();
                //var filteredData = await result.ToListAsync();
                return result;
            }
        }

        // lọc các lịch sử làm việc 
        public IEnumerable<EmploymentWorkingTimeDto> FilterEmployeeDataByVacation(IEnumerable<EmploymentWorkingTimeDto> data, EmployeeFilterDto? filter)
        {
            var filteredData = data.Where(wk =>
                    wk.YearWorking.HasValue && wk.MonthWorking.HasValue &&
                    (!filter.Year.HasValue || wk.YearWorking?.Year == filter.Year) &&
                    (!filter.Month.HasValue || wk.MonthWorking == filter.Month)
            ).ToList();
            return filteredData;
        }

        public async Task<IEnumerable<JobHistoryDto>> FetchJobHistories(EmployeeFilterDto filter)
        {
            using (var sqlServerContext = new SqlServerContext()) // Tạo phiên bản DbContext mới
            {
                var query = sqlServerContext.JobHistory.AsQueryable();
                if (filter.Year.HasValue)
                {
                    query = query.Where(jh => jh.FromDate.HasValue && jh.FromDate.Value.Year == filter.Year.Value);
                }
                if (filter.Month.HasValue)
                {
                    query = query.Where(jh => jh.FromDate.HasValue && jh.FromDate.Value.Month == filter.Month.Value);
                }              
                var result = await query.Select(jh => new JobHistoryDto
                {
                    EmploymentId = jh.EmploymentId,
                    FromDate = jh.FromDate,
                    Department = jh.Department,
                    JobTitle = jh.JobTitle,
                    ThruDate = jh.ThruDate,
                    Location = jh.Location,
                    TypeOfWork = jh.TypeOfWork,
                }).ToListAsync();

                return result;
            }
        }

        // lọc lịch sử làm việc của nhân viên dựa vào endate
        public IEnumerable<EmploymentSqlServerDto> FilterJobHistorySqlServe(IEnumerable<EmploymentSqlServerDto> data, EmployeeFilterDto filter)
        {
            var query = data.Where(p => p.JobHistories.Any(jb =>
                jb.FromDate.HasValue &&
                ((!filter.Year.HasValue || jb.FromDate.Value.Year == filter.Year.Value) && // Kiểm tra năm nếu có
                (!filter.Month.HasValue || jb.FromDate.Value.Month == filter.Month.Value)))) // Kiểm tra tháng nếu có
                .ToList();
            return query;
        }

        // lấy dữ liệu từ sql server
        public async Task<IEnumerable<EmploymentSqlServerDto>> FetchSqlServerData(EmployeeFilterDto filter)
        {
            var employments = await sqlServerContext.Employments
                .Select(e => new EmploymentSqlServerDto
                {
                    EmploymentId = e.EmploymentId,
                    LastName = e.Personal.CurrentLastName,
                    FirstName = e.Personal.CurrentFirstName,
                    ShareholderStatus = e.Personal.ShareholderStatus,
                    BenefitPlan = e.Personal.BenefitPlan.Deductable,
                    NumberDaysRequirementOfWorkingPerMonth = e.NumberDaysRequirementOfWorkingPerMonth,
                    EmploymentStatus = e.EmploymentStatus,
                    Gender = e.Personal.CurrentGender,
                    Ethnicity = e.Personal.Ethnicity,
                    Personal = e.Personal,
                }).ToListAsync();
            var workingTimes = await FetchWorkingTimes(filter);
            var jobHistories = await FetchJobHistories(filter);

            var workingTimesDict = workingTimes?.GroupBy(wt => wt.EmploymentId)?
                                       .ToDictionary(g => g.Key, g => g.ToList());
            var jobHistoriesDict = jobHistories?.GroupBy(jh => jh.EmploymentId)?
                                               .ToDictionary(g => g.Key, g => g.ToList());

            employments = employments.Where(emp =>
                workingTimesDict.ContainsKey(emp.EmploymentId) || jobHistoriesDict.ContainsKey(emp.EmploymentId)).ToList();
            foreach (var employment in employments)
            {
                if (workingTimesDict.TryGetValue(employment.EmploymentId, out var wtList))
                {
                    employment.WorkingTime = wtList;
                }
                else
                {
                    employment.WorkingTime = new List<EmploymentWorkingTimeDto>();
                }
                if (jobHistoriesDict.TryGetValue(employment.EmploymentId, out var jhList))
                {
                    employment.JobHistories = jhList;
                }
                else
                {
                    employment.JobHistories = new List<JobHistoryDto>();
                }
            }
            return employments;
        }
    }
}
