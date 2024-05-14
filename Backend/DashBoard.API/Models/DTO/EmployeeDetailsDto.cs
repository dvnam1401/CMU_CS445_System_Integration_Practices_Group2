namespace DashBoard.API.Models.DTO
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public uint EmploymentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Ssn { get; set; }
        public string EmploymentStatus { get; set; }
        public DateOnly? HireDateForWorking { get; set; }
        public string Department { get; set; }
        public string PayRate { get; set; }
        public int PayRatesIdPayRates { get; set; }
    }
}
