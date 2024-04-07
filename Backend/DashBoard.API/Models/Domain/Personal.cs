using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models.Domain
{
    [Table("Personal")]
    [Index(nameof(BenefitPlans), Name = "IX_Benefit_Plans")]
    public partial class Personal
    {
        public Personal()
        {
            JobHistories = new HashSet<JobHistory>();
        }

        [Key]
        [Column("Employee_ID", TypeName = "numeric(18, 0)")]
        public decimal EmployeeId { get; set; }
        [Column("First_Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("Last_Name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Column("Middle_Initial")]
        [StringLength(50)]
        public string MiddleInitial { get; set; }
        [StringLength(50)]
        public string Address1 { get; set; }
        [StringLength(50)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? Zip { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Column("Phone_Number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [Column("Social_Security_Number")]
        [StringLength(50)]
        public string SocialSecurityNumber { get; set; }
        [Column("Drivers_License")]
        [StringLength(50)]
        public string DriversLicense { get; set; }
        [Column("Marital_Status")]
        [StringLength(50)]
        public string MaritalStatus { get; set; }
        public bool? Gender { get; set; }
        [Column("Shareholder_Status")]
        public bool ShareholderStatus { get; set; }
        [Column("Benefit_Plans", TypeName = "numeric(18, 0)")]
        public decimal? BenefitPlans { get; set; }
        [StringLength(50)]
        public string Ethnicity { get; set; }

        [ForeignKey(nameof(BenefitPlans))]
        [InverseProperty(nameof(BenefitPlan.Personals))]
        public virtual BenefitPlan BenefitPlansNavigation { get; set; }
        [InverseProperty("Employee")]
        public virtual EmergencyContact EmergencyContact { get; set; }
        [InverseProperty("Employee")]
        public virtual Employment Employment { get; set; }
        [InverseProperty(nameof(JobHistory.Employee))]
        public virtual ICollection<JobHistory> JobHistories { get; set; }
    }
}
