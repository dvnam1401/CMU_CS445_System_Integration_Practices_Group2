using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Admin;

public partial class Group
{
    [Key]
    [Column("group_id")]
    public int GroupId { get; set; }

    [Column("groupName")]
    [StringLength(50)]
    [Unicode(false)]
    public string GroupName { get; set; } = null!;

    [InverseProperty("Group")]
    public virtual ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();

    [InverseProperty("Group")]
    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}
