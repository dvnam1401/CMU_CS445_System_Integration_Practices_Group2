using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[PrimaryKey("EmployeeNumber", "PayRatesIdPayRates")]
[Table("employee")]
[Index("EmployeeNumber", Name = "Employee Number_UNIQUE", IsUnique = true)]
[Index("PayRatesIdPayRates", Name = "fk_Employee_Pay Rates")]
public partial class Employee
{
    [Column("idEmployee")]
    public int IdEmployee { get; set; }

    [Key]
    [Column("Employee Number")]
    public uint EmployeeNumber { get; set; }

    [Column("Last Name")]
    [StringLength(45)]
    public string LastName { get; set; } = null!;

    [Column("First Name")]
    [StringLength(45)]
    public string FirstName { get; set; } = null!;

    [Column("SSN")]
    [Precision(10, 0)]
    public decimal Ssn { get; set; }

    [Column("Pay Rate")]
    [StringLength(40)]
    public string? PayRate { get; set; }

    [Key]
    [Column("Pay Rates_idPay Rates")]
    public int PayRatesIdPayRates { get; set; }

    [Column("Vacation Days")]
    public int? VacationDays { get; set; }

    [Column("Paid To Date")]
    [Precision(2, 0)]
    public decimal? PaidToDate { get; set; }

    [Column("Paid Last Year")]
    [Precision(2, 0)]
    public decimal? PaidLastYear { get; set; }

    [InverseProperty("EmployeeNumberNavigation")]
    public virtual Birthday? Birthday { get; set; }

    [ForeignKey("PayRatesIdPayRates")]
    [InverseProperty("Employees")]
    public virtual PayRate PayRatesIdPayRatesNavigation { get; set; } = null!;
}
