using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[Table("pay rates")]
public partial class PayRate
{
    [Key]
    [Column("idPay Rates")]
    public int IdPayRates { get; set; }

    [Column("Pay Rate Name")]
    [StringLength(40)]
    public string PayRateName { get; set; } = null!;

    [Precision(10, 0)]
    public decimal Value { get; set; }

    [Column("Tax Percentage")]
    [Precision(10, 0)]
    public decimal TaxPercentage { get; set; }

    [Column("Pay Type")]
    public int PayType { get; set; }

    [Column("Pay Amount")]
    [Precision(10, 0)]
    public decimal PayAmount { get; set; }

    [Column("PT - Level C")]
    [Precision(10, 0)]
    public decimal PtLevelC { get; set; }

    [InverseProperty("PayRatesIdPayRatesNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
