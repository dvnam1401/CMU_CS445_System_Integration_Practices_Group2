namespace DashBoard.API.Models.DTO
{
    public class PayRollUpdateEmployeeDto
    {
        public int EmployeeNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public decimal Ssn { get; set; }
        public int PayRatesIdPayRates { get; set; }
    }
}
