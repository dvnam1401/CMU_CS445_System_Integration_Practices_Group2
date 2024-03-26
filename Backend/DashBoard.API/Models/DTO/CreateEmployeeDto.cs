namespace DashBoard.API.Models.DTO
{
    public class CreateEmployeeDto
    {
        public int IdEmployee { get; set; }
        public int EmployeeNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? Dateofbirthday { get; set; }
        public decimal Ssn { get; set; }
        public int PayRatesIdPayRates { get; set; }
        public string PhoneNumber { get; set; }
        public bool ShareholderStatus { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }       
    }
}
