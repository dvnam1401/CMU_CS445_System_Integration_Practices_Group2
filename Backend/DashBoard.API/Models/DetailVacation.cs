using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models
{
    [Table("detail vacation")]
    public partial class DetailVacation
    {
        [Column("dayoff", TypeName = "date")]
        public DateTime? Dayoff { get; set; }
        [Column("resignation content")]
        [StringLength(500)]
        public string ResignationContent { get; set; }
        [Key]
        [Column("employeeNumber", TypeName = "int unsigned")]
        public int EmployeeNumber { get; set; }

        public virtual Employee EmployeeNumberNavigation { get; set; }
    }
}
