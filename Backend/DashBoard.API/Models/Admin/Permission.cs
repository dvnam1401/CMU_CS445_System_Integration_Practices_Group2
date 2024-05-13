using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Admin;

public partial class Permission
{
    [Key]
    [Column("permission_id")]
    public int PermissionId { get; set; }

    [Column("permisstion_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PermisstionName { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("Permission")]
    public virtual ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
}
