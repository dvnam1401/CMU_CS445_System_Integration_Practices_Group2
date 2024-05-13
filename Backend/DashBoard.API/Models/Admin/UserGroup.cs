using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Admin;

[PrimaryKey("UserId", "GroupId")]
[Index("GroupId", Name = "IX_UserGroups_GroupId")]
public partial class UserGroup
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Key]
    [Column("group_id")]
    public int GroupId { get; set; }

    [Column("is_enable")]
    public bool? IsEnable { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("UserGroups")]
    public virtual Group Group { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserGroups")]
    public virtual AccountUser User { get; set; } = null!;
}
