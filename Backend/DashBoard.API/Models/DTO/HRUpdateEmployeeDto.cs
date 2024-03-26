namespace DashBoard.API.Models.DTO
{
    public class HRUpdateEmployeeDto
    {
        public decimal EmployeeId { get; set; } //EmployeeNumber
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Dateofbirthday { get; set; }
        public bool ShareholderStatus { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
    }
}
