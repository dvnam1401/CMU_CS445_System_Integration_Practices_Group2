using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationEmployee>> GetEmployeesAnniversaryInfo(int daysLimit);
        Task<IEnumerable<NotificationEmployee>> GetEmployeesWithAccumulatedVacationDays(int minimumDays);
        Task<IEnumerable<NotificationEmployee>> GetEmployeesWithBirthdaysThisMonth(int daysLimit);
    }
}
