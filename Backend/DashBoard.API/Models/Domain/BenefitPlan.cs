using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models.Domain
{
    [Table("Benefit_Plans")]
    public partial class BenefitPlan
    {
        public BenefitPlan()
        {
            Personals = new HashSet<Personal>();
        }

        [Key]
        [Column("Benefit_Plan_ID", TypeName = "numeric(18, 0)")]
        public decimal BenefitPlanId { get; set; }
        [Column("Plan_Name")]
        [StringLength(50)]
        public string PlanName { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? Deductable { get; set; }
        [Column("Percentage_CoPay")]
        public int? PercentageCoPay { get; set; }

        [InverseProperty(nameof(Personal.BenefitPlansNavigation))]
        public virtual ICollection<Personal> Personals { get; set; }
    }
}
