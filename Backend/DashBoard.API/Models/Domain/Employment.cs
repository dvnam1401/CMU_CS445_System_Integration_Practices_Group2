using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[Table("EMPLOYMENT")]
public partial class Employment
{
    [Key]
    [Column("EMPLOYMENT_ID", TypeName = "numeric(18, 0)")]
    public decimal EmploymentId { get; set; }

    [Column("EMPLOYMENT_CODE")]
    [StringLength(50)]
    public string? EmploymentCode { get; set; }

    [Column("EMPLOYMENT_STATUS")]
    [StringLength(10)]
    public string? EmploymentStatus { get; set; }

    [Column("HIRE_DATE_FOR_WORKING")]
    public DateOnly? HireDateForWorking { get; set; }

    /// <summary>
    /// MÃ CÔNG VIỆC
    /// </summary>
    [Column("WORKERS_COMP_CODE")]
    [StringLength(10)]
    public string? WorkersCompCode { get; set; }

    [Column("TERMINATION_DATE")]
    public DateOnly? TerminationDate { get; set; }

    [Column("REHIRE_DATE_FOR_WORKING")]
    public DateOnly? RehireDateForWorking { get; set; }

    [Column("LAST_REVIEW_DATE")]
    public DateOnly? LastReviewDate { get; set; }

    [Column("NUMBER_DAYS_REQUIREMENT_OF_WORKING_PER_MONTH", TypeName = "numeric(18, 0)")]
    public decimal? NumberDaysRequirementOfWorkingPerMonth { get; set; }

    [Column("PERSONAL_ID", TypeName = "numeric(18, 0)")]
    public decimal? PersonalId { get; set; }

    [InverseProperty("Employment")]
    public virtual ICollection<EmploymentWorkingTime> EmploymentWorkingTimes { get; set; } = new List<EmploymentWorkingTime>();

    [InverseProperty("Employment")]
    public virtual ICollection<JobHistory> JobHistories { get; set; } = new List<JobHistory>();

    [ForeignKey("PersonalId")]
    [InverseProperty("Employments")]
    public virtual Personal? Personal { get; set; }
}
