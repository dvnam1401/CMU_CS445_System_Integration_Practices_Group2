using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Admin;

[PrimaryKey("GroupId", "PermissionId")]
[Index("PermissionId", Name = "IX_GroupPermissions_PermissionId")]
public partial class GroupPermission
{
    [Key]
    [Column("group_id")]
    public int GroupId { get; set; }

    [Key]
    [Column("permission_id")]
    public int PermissionId { get; set; }

    [Column("is_enable")]
    public bool? IsEnable { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("GroupPermissions")]
    public virtual Group Group { get; set; } = null!;

    [ForeignKey("PermissionId")]
    [InverseProperty("GroupPermissions")]
    public virtual Permission Permission { get; set; } = null!;
}
