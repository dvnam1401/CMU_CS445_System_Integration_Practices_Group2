using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models
{
    [Table("birthday")]
    public partial class Birthday
    {
        [Column("dateofbirthday", TypeName = "date")]
        public DateTime? Dateofbirthday { get; set; }
        [Key]
        [Column("employeeNumber", TypeName = "int unsigned")]
        public int EmployeeNumber { get; set; }

        public virtual Employee EmployeeNumberNavigation { get; set; }
    }
}
