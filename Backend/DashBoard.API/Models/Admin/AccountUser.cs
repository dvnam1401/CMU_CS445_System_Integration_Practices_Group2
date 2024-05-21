using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Admin;

[Table("AccountUser")]
public partial class AccountUser
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("first_name")]
    [StringLength(100)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(100)]
    public string? LastName { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("department")]
    [StringLength(50)]
    public string? Department { get; set; }

    [Column("user_name")]
    [StringLength(256)]
    public string? UserName { get; set; }

    [Column("email")]
    [StringLength(256)]
    public string? Email { get; set; }

    [Column("password_hash")]
    public string? PasswordHash { get; set; }

    [Column("phone_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    [InverseProperty("User")]
    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
