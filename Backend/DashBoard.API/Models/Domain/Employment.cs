using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models.Domain
{
    [Table("Employment")]
    [Index(nameof(EmployeeId), Name = "IX_Employee_ID")]
    public partial class Employment
    {
        [Key]
        [Column("Employee_ID", TypeName = "numeric(18, 0)")]
        public decimal EmployeeId { get; set; }
        [Column("Employment_Status")]
        [StringLength(50)]
        public string EmploymentStatus { get; set; }
        [Column("Hire_Date", TypeName = "datetime")]
        public DateTime? HireDate { get; set; }
        [Column("Workers_Comp_Code")]
        [StringLength(50)]
        public string WorkersCompCode { get; set; }
        [Column("Termination_Date", TypeName = "datetime")]
        public DateTime? TerminationDate { get; set; }
        [Column("Rehire_Date", TypeName = "datetime")]
        public DateTime? RehireDate { get; set; }
        [Column("Last_Review_Date", TypeName = "datetime")]
        public DateTime? LastReviewDate { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(Personal.Employment))]
        public virtual Personal Employee { get; set; }
    }
}
