using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Admin;

public partial class UserPermission
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("group_id")]
    public int GroupId { get; set; }

    [Column("group_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? GroupName { get; set; }

    [Column("permission_id")]
    public int PermissionId { get; set; }

    [Column("permisison_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string PermisisonName { get; set; } = null!;

    [Column("is_enable")]
    public bool? IsEnable { get; set; }

    [Key]
    [Column("id_user_role")]
    public int IdUserRole { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserPermissions")]
    public virtual AccountUser User { get; set; } = null!;
}
