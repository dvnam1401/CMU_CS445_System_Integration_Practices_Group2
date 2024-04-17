using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[Table("EMPLOYMENT_WORKING_TIME")]
public partial class EmploymentWorkingTime
{
    [Key]
    [Column("EMPLOYMENT_WORKING_TIME_ID", TypeName = "numeric(18, 0)")]
    public decimal EmploymentWorkingTimeId { get; set; }

    [Column("EMPLOYMENT_ID", TypeName = "numeric(18, 0)")]
    public decimal? EmploymentId { get; set; }

    [Column("YEAR_WORKING")]
    public DateOnly? YearWorking { get; set; }

    [Column("MONTH_WORKING", TypeName = "numeric(2, 0)")]
    public decimal? MonthWorking { get; set; }

    [Column("NUMBER_DAYS_ACTUAL_OF_WORKING_PER_MONTH", TypeName = "numeric(2, 0)")]
    public decimal? NumberDaysActualOfWorkingPerMonth { get; set; }

    [Column("TOTAL_NUMBER_VACATION_WORKING_DAYS_PER_MONTH", TypeName = "numeric(2, 0)")]
    public decimal? TotalNumberVacationWorkingDaysPerMonth { get; set; }

    [ForeignKey("EmploymentId")]
    [InverseProperty("EmploymentWorkingTimes")]
    public virtual Employment? Employment { get; set; }
}
