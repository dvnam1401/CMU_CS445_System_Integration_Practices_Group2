using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models
{
    [Table("pay rates")]
    public partial class PayRate
    {
        public PayRate()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Column("idPay Rates")]
        public int IdPayRates { get; set; }
        [Required]
        [Column("Pay Rate Name")]
        [StringLength(40)]
        public string PayRateName { get; set; }
        [Column(TypeName = "decimal(10,0)")]
        public decimal Value { get; set; }
        [Column("Tax Percentage", TypeName = "decimal(10,0)")]
        public decimal TaxPercentage { get; set; }
        [Column("Pay Type")]
        public int PayType { get; set; }
        [Column("Pay Amount", TypeName = "decimal(10,0)")]
        public decimal PayAmount { get; set; }
        [Column("PT - Level C", TypeName = "decimal(10,0)")]
        public decimal PtLevelC { get; set; }

        [InverseProperty(nameof(Employee.PayRatesIdPayRatesNavigation))]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
