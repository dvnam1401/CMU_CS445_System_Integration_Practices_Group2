using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard.API.Models.Domain
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Column("idEmployee")]
        public int employeeId { get; set; }

        [Column("Employee Number")]
        public int employeeNumber { get; set; }

        [Column("Last Name")]
        [MaxLength(45)]
        public string lastName { get; set; }

        [Column("First Name")]
        [MaxLength(45)]
        public string firstName { get; set; }

        [Column("SSN")]
        public decimal SSN { get; set; } // Hoặc sử dụng string nếu SSN được lưu với định dạng có dấu phân cách

        [Column("Pay Rate")]
        [MaxLength(40)]
        public string payRate { get; set; }

        [Column("Pay Rates_idPay Rates")]
        public int payRatesIdPayRates { get; set; }

        [Column("Vacation Days")]
        public int vacationDays { get; set; }

        [Column("Paid To Date")]
        public decimal paidToDate { get; set; }

        [Column("Paid Last Year")]
        public decimal paidLastYear { get; set; }

        //[ForeignKey("PayRatesIdPayRates")]
        public virtual PayRate PayRates { get; set; }
    }
}
