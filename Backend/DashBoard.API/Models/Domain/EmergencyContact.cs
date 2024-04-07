using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models.Domain
{
    [Table("Emergency_Contacts")]
    [Index(nameof(EmployeeId), Name = "IX_Employee_ID")]
    public partial class EmergencyContact
    {
        [Key]
        [Column("Employee_ID", TypeName = "numeric(18, 0)")]
        public decimal EmployeeId { get; set; }
        [Column("Emergency_Contact_Name")]
        [StringLength(50)]
        public string EmergencyContactName { get; set; }
        [Column("Phone_Number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Relationship { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(Personal.EmergencyContact))]
        public virtual Personal Employee { get; set; }
    }
}
