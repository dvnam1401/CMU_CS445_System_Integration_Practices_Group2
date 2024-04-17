using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[Table("JOB_HISTORY")]
public partial class JobHistory
{
    [Key]
    [Column("JOB_HISTORY_ID", TypeName = "numeric(18, 0)")]
    public decimal JobHistoryId { get; set; }

    [Column("EMPLOYMENT_ID", TypeName = "numeric(18, 0)")]
    public decimal? EmploymentId { get; set; }

    [Column("DEPARTMENT")]
    [StringLength(250)]
    public string? Department { get; set; }

    [Column("DIVISION")]
    [StringLength(250)]
    public string? Division { get; set; }

    [Column("FROM_DATE")]
    public DateOnly? FromDate { get; set; }

    [Column("THRU_DATE")]
    public DateOnly? ThruDate { get; set; }

    [Column("JOB_TITLE")]
    [StringLength(250)]
    public string? JobTitle { get; set; }

    [Column("SUPERVISOR")]
    [StringLength(250)]
    public string? Supervisor { get; set; }

    [Column("LOCATION")]
    [StringLength(250)]
    public string? Location { get; set; }

    [Column("TYPE_OF_WORK")]
    public short? TypeOfWork { get; set; }

    [ForeignKey("EmploymentId")]
    [InverseProperty("JobHistories")]
    public virtual Employment? Employment { get; set; }
}
