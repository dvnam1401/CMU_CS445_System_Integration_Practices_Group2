using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard.API.Models.Domain
{
    public class BenefitPlan
    {
        [Key]
        [Column("Benefit_Plan_ID")]
        public decimal benefitId { get; set; }
        [Column("Plan_Name")]
        public string planName { get; set; }
        [Column("Deductable")]
        public decimal deductable { get; set; }

        [Column("Percentage_CoPay")]
        public int PercentageCopay { get; set; }

        public ICollection<Personal> Personals { get; set; }
    }
}
