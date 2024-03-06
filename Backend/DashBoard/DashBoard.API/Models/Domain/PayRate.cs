using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DashBoard.API.Models.Domain
{
    [Table("Pay Rates")]
    public class PayRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idPay Rates")]
        public int PayRatesId { get; set; }

        [Column("Pay Rate Name")]
        [MaxLength(40)]
        public string payRateName { get; set; }

        [Column("Value")]
        public decimal value { get; set; }

        [Column("Tax Percentage")]
        public decimal taxPercentage { get; set; }

        [Column("Pay Type")]
        public int payType { get; set; }

        [Column("Pay Amount")]
        public decimal pyAmount { get; set; }

        [Column("PT - Level C")]
        public decimal PTLevel { get; set; }

        // Mối quan hệ với Employee
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
