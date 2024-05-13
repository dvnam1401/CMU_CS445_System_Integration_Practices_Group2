using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;

namespace DashBoard.API.Repositories.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMysqlDataRepository mysqlDataRepository;
        private readonly ISqlDataRepository sqlDataRepository;

        public NotificationRepository(IMysqlDataRepository mysqlDataRepository, ISqlDataRepository sqlDataRepository)
        {

            this.mysqlDataRepository = mysqlDataRepository;
            this.sqlDataRepository = sqlDataRepository;
        }
        // lấy ra số ngày kỉ niệm của nhân viên
        public async Task<IEnumerable<NotificationEmployee>> GetEmployeesAnniversaryInfo(int daysLimit)
        {
            var today = DateTime.Today;
            var employments = await sqlDataRepository.FetchEmployments();

            // Trước tiên, lọc danh sách để chỉ giữ lại những đối tượng có HireDate và số ngày đến ngày kỷ niệm nằm trong phạm vi được truyền vào
            var employeeAnniversary = employments
                .Where(e => e.HireDateForWorking is not null)
                .Select(e =>
                {
                    var hireDateThisYear = DateTime.Now;
                    if (e.RehireDateForWorking is not null)
                    {
                        hireDateThisYear = new DateTime(today.Year, e.RehireDateForWorking.Value.Month, e.RehireDateForWorking.Value.Day);
                    }
                    else
                    {
                        hireDateThisYear = new DateTime(today.Year, e.HireDateForWorking.Value.Month, e.HireDateForWorking.Value.Day);

                    }
                    // Tính ngày kỷ niệm trong năm hiện tại

                    // Nếu ngày kỷ niệm đã qua, chuyển sang năm tiếp theo
                    if (hireDateThisYear < today)
                    {
                        hireDateThisYear = hireDateThisYear.AddYears(1);
                    }
                    var daysUntilNextAnniversary = (hireDateThisYear - today).Days;

                    // Kiểm tra số ngày còn lại có trong phạm vi được yêu cầu không
                    if (daysUntilNextAnniversary > daysLimit)
                    {
                        return null; // Loại bỏ những nhân viên không trong phạm vi
                    }
                    var content = $"Số ngày còn lại trước khi tới ngày kỷ niệm: {daysUntilNextAnniversary}";
                    var department = e.Department ?? "Không rõ";

                    return new NotificationEmployee
                    {
                        FullName = $"{e.LastName} {e.FirstName}",
                        Department = department,
                        Content = content
                    };
                }).Where(e => e is not null);
            //{
            //    if (e.Employments?.HireDate is null) return false;
            //    var hireDateThisYear = new DateTime(today.Year, e.Employments.HireDate.Value.Month, e.Employments.HireDate.Value.Day);
            //    if (hireDateThisYear < today) hireDateThisYear = hireDateThisYear.AddYears(1); // Chuyển sang năm tiếp theo nếu đã qua
            //    var daysUntilNextAnniversary = (hireDateThisYear - today).Days;
            //    return daysUntilNextAnniversary <= daysLimit;
            return employeeAnniversary;
        }

        public async Task<IEnumerable<NotificationEmployee>> GetEmployeesWithAccumulatedVacationDays(int minimumDays)
        {
            var employmentTask = sqlDataRepository.FetchEmployments();
            var workingTimeTask = sqlDataRepository.FetchWorkingTimes(minimumDays);
            await Task.WhenAll(employmentTask, workingTimeTask);

            var employments = await employmentTask;
            var workingTimes = await workingTimeTask;
            //var filterYear = DateTime.Today.Year;
            EmployeeFilterDto filter = new EmployeeFilterDto
            {
                Year = DateTime.Today.Year,
                Month = null,
            };
            var employeesWithAccumulatedDays = JoinAndTransformDataAccumulatedVacationDays(employments, workingTimes);
            return employeesWithAccumulatedDays;
        }

        public IEnumerable<NotificationEmployee> JoinAndTransformDataAccumulatedVacationDays(IEnumerable<EmploymentDto> employments, IEnumerable<EmploymentWorkingTimeDto> workingTimes)
        {
            return from e in employments
                   join wkt in workingTimes on e.EmploymentId equals wkt.EmploymentId
                   select new NotificationEmployee
                   {
                       FullName = $"{e.FirstName} {e.LastName}",
                       Department = e.Department,
                       Content = $"Đã nghỉ {wkt.TotalNumberVacationWorkingDaysPerMonth} ngày, quá số ngày nghỉ phép quy định ",
                   };
        }
        public async Task<IEnumerable<NotificationEmployee>> GetEmployeesWithBirthdaysThisMonth(int daysLimit)
        {
            var brithdayTask = sqlDataRepository.FetchEmployments();
            var birthdays = await brithdayTask;

            var today = DateTime.Today;
            var employeesWithBirthdays = birthdays
                .Where(e => e.Birthday is not null)
                .Where(e => e.Birthday.Value.Month == today.Month)
                .Select(e =>
                {
                    var birthdayThisYear = new DateTime(today.Year, e.Birthday.Value.Month, e.Birthday.Value.Day);
                    if (birthdayThisYear < today)
                    {
                        birthdayThisYear = birthdayThisYear.AddYears(1);
                    }

                    var daysUntilNextBirthday = (birthdayThisYear - today).Days;

                    // Kiểm tra số ngày còn lại có trong phạm vi được yêu cầu không
                    if (daysUntilNextBirthday > daysLimit)
                    {
                        return null; // Loại bỏ những nhân viên không trong phạm vi
                    }

                    var countBirthday = (birthdayThisYear - today).Days;
                    var content = $"Còn {countBirthday} là tới ngày sinh nhật";
                    var department = e.Department;
                    return new NotificationEmployee
                    {
                        FullName = $"{e.LastName} {e.FirstName}",
                        Department = department,
                        Content = content
                    };
                }).Where(e => e is not null);
            return employeesWithBirthdays;
        }
    }
}
