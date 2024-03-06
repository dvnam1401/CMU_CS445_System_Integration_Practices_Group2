using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard.API.Models.Domain
{
    [Table("Employment")]
    public class Employment
    {
        [Key, ForeignKey("Personal")]
        [Column("Employee_Id")]
        public decimal employeeId { get; set; }
        [Column("Employment_Status", TypeName = "nvarchar(50)")]

        public string employmentStatus { get; set; }
        [Column("Hire_Date")]
        public DateTime? hireDate { get; set; }
        [Column("Workers_Comp_Code", TypeName = "nvarchar(50)")]
        public string workersCompCode { get; set; }
        [Column("Termination_Date")]

        public DateTime? terminationDate { get; set; }
        [Column("Rehire_Date")]

        public DateTime? rehireDate { get; set; }
        [Column("Last_Review_Date")]

        public DateTime? lastReviewDate { get; set; }

        public virtual Personal Personal { get; set; }
    }
}
