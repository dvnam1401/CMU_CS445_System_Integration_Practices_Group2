using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[Table("BENEFIT_PLANS")]
public partial class BenefitPlan
{
    [Key]
    [Column("BENEFIT_PLANS_ID", TypeName = "numeric(18, 0)")]
    public decimal BenefitPlansId { get; set; }

    [Column("PLAN_NAME")]
    [StringLength(10)]
    public string? PlanName { get; set; }

    [Column("DEDUCTABLE", TypeName = "money")]
    public decimal? Deductable { get; set; }

    [Column("PERCENTAGE_COPAY", TypeName = "numeric(18, 0)")]
    public decimal? PercentageCopay { get; set; }

    [InverseProperty("BenefitPlan")]
    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
