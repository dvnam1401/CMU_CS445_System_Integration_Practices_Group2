namespace DashBoard.API.Models.Inteface
{
    public interface INotificationEmployee
    {
        public string? FullName { get; set; }
        public string? Department { get; set; }
        public string? Content { get; set; }

    }
}
