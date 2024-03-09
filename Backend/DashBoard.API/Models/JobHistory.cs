using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models
{
    [Table("Job_History")]
    [Index(nameof(EmployeeId), Name = "IX_Employee_ID")]
    public partial class JobHistory
    {
        [Key]
        [Column("ID", TypeName = "numeric(18, 0)")]
        public decimal Id { get; set; }
        [Column("Employee_ID", TypeName = "numeric(18, 0)")]
        public decimal EmployeeId { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        [StringLength(50)]
        public string Division { get; set; }
        [Column("Start_Date", TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column("End_Date", TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("Job_Title")]
        [StringLength(50)]
        public string JobTitle { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? Supervisor { get; set; }
        [Column("Job_Category")]
        [StringLength(50)]
        public string JobCategory { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        [Column("Departmen_Code", TypeName = "numeric(18, 0)")]
        public decimal? DepartmenCode { get; set; }
        [Column("Salary_Type", TypeName = "numeric(18, 0)")]
        public decimal? SalaryType { get; set; }
        [Column("Pay_Period")]
        [StringLength(50)]
        public string PayPeriod { get; set; }
        [Column("Hours_per_Week", TypeName = "numeric(18, 0)")]
        public decimal? HoursPerWeek { get; set; }
        [Column("Hazardous_Training")]
        public bool? HazardousTraining { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(Personal.JobHistories))]
        public virtual Personal Employee { get; set; }
    }
}
