using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DashBoard.API.Models.Domain
{
    [Table("Emergency_Contacts")]
    public class EmergencyContact
    {
        [Key, ForeignKey("Personal")]
        [Column("Employee_ID")]
        public decimal EmployeeID { get; set; }

        [Column("Emergency_Contact_Name", TypeName = "nvarchar(50)")]
        public string emergencyContactName { get; set; }

        [Column("Phone_Number", TypeName = "nvarchar(50)")]
        public string phoneNumber { get; set; }

        [Column("Relationship", TypeName = "nvarchar(50)")]
        public string relationship { get; set; }

        public virtual Personal Personal { get; set; }
    }
}
