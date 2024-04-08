using DashBoard.API.Models.Inteface;

namespace DashBoard.API.Models.DTO
{
    public class EmployeeBirthdayDto : INotificationEmployee
    {
        public string? FullName { get; set; }
        public string? Department { get; set; }
        public string? Content { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
