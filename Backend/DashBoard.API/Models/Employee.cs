using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models
{
    [Table("employee")]
    [Index(nameof(EmployeeNumber), Name = "Employee Number_UNIQUE", IsUnique = true)]
    [Index(nameof(PayRatesIdPayRates), Name = "fk_Employee_Pay Rates")]
    public partial class Employee
    {
        [Column("idEmployee")]
        public int IdEmployee { get; set; }
        [Key]
        [Column("Employee Number", TypeName = "int unsigned")]
        public int EmployeeNumber { get; set; }
        [Required]
        [Column("Last Name")]
        [StringLength(45)]
        public string LastName { get; set; }
        [Required]
        [Column("First Name")]
        [StringLength(45)]
        public string FirstName { get; set; }
        [Column("SSN", TypeName = "decimal(10,0)")]
        public decimal Ssn { get; set; }
        [Column("Pay Rate")]
        [StringLength(40)]
        public string PayRate { get; set; }
        [Key]
        [Column("Pay Rates_idPay Rates")]
        public int PayRatesIdPayRates { get; set; }
        [Column("Vacation Days")]
        public int? VacationDays { get; set; }
        [Column("Paid To Date", TypeName = "decimal(2,0)")]
        public decimal? PaidToDate { get; set; }
        [Column("Paid Last Year", TypeName = "decimal(2,0)")]
        public decimal? PaidLastYear { get; set; }

        [ForeignKey(nameof(PayRatesIdPayRates))]
        [InverseProperty("Employees")]
        public virtual PayRate PayRatesIdPayRatesNavigation { get; set; }
    }
}
