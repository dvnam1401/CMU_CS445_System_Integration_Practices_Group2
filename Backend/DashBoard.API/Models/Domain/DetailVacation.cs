using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[Table("detail vacation")]
[Index("EmployeeNumber", Name = "employeeNumber")]
public partial class DetailVacation
{
    [Key]
    [Column("VacationID")]
    public int VacationId { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Dayoff { get; set; }

    [Column("resignation content")]
    [StringLength(500)]
    public string? ResignationContent { get; set; }

    [Column("employeeNumber")]
    public uint EmployeeNumber { get; set; }
}
