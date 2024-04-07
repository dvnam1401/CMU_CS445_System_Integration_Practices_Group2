using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DashBoard.API.Models.Domain;

namespace DashBoard.API.Models.DTO
{
    public class EmployeeDto
    {
        public uint EmployeeNumber { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public decimal Ssn { get; set; }
        public string? PayRate { get; set; }
        public int PayRatesIdPayRates { get; set; }
        public int? VacationDays { get; set; }
        public decimal? PaidToDate { get; set; }
        public decimal? PaidLastYear { get; set; }
        public PayRate? PayRates { get; set; }
    }
}
