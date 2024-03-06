using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard.API.Models.Domain
{
    [Table("Job_History")]
    public class JobHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal ID { get; set; }
        [ForeignKey("Personal")]
        [Column("Employee_ID")]
        public decimal employeeID { get; set; }
        [Column("Department", TypeName = "nvarchar(50)")]
        public string department { get; set; }
        [Column("Division", TypeName = "nvarchar(50)")]
        public string division { get; set; }
        [Column("Start_Date")]
        public DateTime? startDate { get; set; }
        [Column("End_Date")]
        public DateTime? endDate { get; set; }
        [Column("Job_Title", TypeName = "nvarchar(50)")]
        public string jobTitle { get; set; }
        [Column("Supervisor")]
        public decimal? supervisor { get; set; }
        [Column("Job_Category", TypeName = "nvarchar(50)")]
        public string jobCategory { get; set; }

        [Column("Location", TypeName = "nvarchar(50)")]
        public string location { get; set; }
        [Column("Departmen_Code")]

        public decimal? departmentCode { get; set; }
        [Column("Salary_Type")]
        public decimal? salaryType { get; set; }
        [Column("Pay_Period")]
        public decimal? payPeriod { get; set; }
        [Column("Hours_per_Week")]
        public decimal? hoursPerWeek { get; set; }
        [Column("Hazardous_Training")]
        public bool? hazardousTraining { get; set; }

        public virtual Personal Personal { get; set; }
    }
}
