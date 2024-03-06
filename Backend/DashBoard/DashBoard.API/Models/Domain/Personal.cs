using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard.API.Models.Domain
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        [Column("Employee_ID")]
        public decimal employeeId { get; set; }
        [Column("First_Name", TypeName = "nvarchar(50)")]
        public string firstName { get; set; }
        [Column("Last_Name", TypeName = "nvarchar(50)")]
        public string lastName { get; set; }
        [Column("Middle_Initial", TypeName = "nvarchar(50)")]
        public string middleInitial { get; set; }
        [Column("Address1", TypeName = "nvarchar(50)")]
        public string address1 { get; set; }
        [Column("Address2", TypeName = "nvarchar(50)")]
        public string address2 { get; set; }
        [Column("City", TypeName = "nvarchar(50)")]

        public string city { get; set; }
        [Column("State", TypeName = "nvarchar(50)")]
        public string state { get; set; }
        [Column("Zip")]

        public decimal zipcode { get; set; }
        [Column("Email", TypeName = "nvarchar(50)")]
        public string email { get; set; }
        [Column("Phone_Number", TypeName = "nvarchar(50)")]
        public string phone { get; set; }
        [Column("Social_Security_Number", TypeName = "nvarchar(50)")]
        public string securityNumber { get; set; }
        [Column("Drivers_License", TypeName = "nvarchar(50)")]
        public string driverLicense { get; set; }
        [Column("Marital_Status", TypeName = "nvarchar(50)")]
        public string maritalStatus { get; set; }
        [Column("Gender")]
        public bool? gender { get; set; }
        [Column("Shareholder_Status")]
        public bool? shareholderStatus { get; set; }
        [Column("Benefit_Plans")]
        [ForeignKey("Benefit_Plans")]
        public decimal? benefitPlans { get; set; }
        [Column("Ethnicity", TypeName = "nvarchar(50)")]
        public string ethnicity { get; set; }

        public virtual BenefitPlan BenefitPlan { get; set; }
        public virtual Employment Employment { get; set; }
        public virtual ICollection<JobHistory> JobHistories { get; set; }
        public virtual EmergencyContact EmergencyContact { get; set; }
    }
}
