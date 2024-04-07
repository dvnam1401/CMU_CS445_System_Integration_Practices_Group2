using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[Table("birthday")]
public partial class Birthday
{
    [Column("dateofbirthday", TypeName = "date")]
    public DateTime? Dateofbirthday { get; set; }

    [Key]
    [Column("employeeNumber")]
    public uint EmployeeNumber { get; set; }

    [ForeignKey("EmployeeNumber")]
    [InverseProperty("Birthday")]
    public virtual Employee EmployeeNumberNavigation { get; set; } = null!;
}
