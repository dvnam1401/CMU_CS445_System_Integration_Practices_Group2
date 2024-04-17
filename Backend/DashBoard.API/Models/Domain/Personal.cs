using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

[Table("PERSONAL")]
public partial class Personal
{
    [Key]
    [Column("PERSONAL_ID", TypeName = "numeric(18, 0)")]
    public decimal PersonalId { get; set; }

    [Column("CURRENT_FIRST_NAME")]
    [StringLength(50)]
    public string? CurrentFirstName { get; set; }

    [Column("CURRENT_LAST_NAME")]
    public string? CurrentLastName { get; set; }

    [Column("CURRENT_MIDDLE_NAME")]
    [StringLength(50)]
    public string? CurrentMiddleName { get; set; }

    [Column("BIRTH_DATE")]
    public DateOnly? BirthDate { get; set; }

    [Column("SOCIAL_SECURITY_NUMBER")]
    [StringLength(20)]
    public string? SocialSecurityNumber { get; set; }

    [Column("DRIVERS_LICENSE")]
    [StringLength(50)]
    public string? DriversLicense { get; set; }

    [Column("CURRENT_ADDRESS_1")]
    [StringLength(255)]
    public string? CurrentAddress1 { get; set; }

    [Column("CURRENT_ADDRESS_2")]
    [StringLength(255)]
    public string? CurrentAddress2 { get; set; }

    [Column("CURRENT_CITY")]
    [StringLength(100)]
    public string? CurrentCity { get; set; }

    [Column("CURRENT_COUNTRY")]
    [StringLength(100)]
    public string? CurrentCountry { get; set; }

    [Column("CURRENT_ZIP", TypeName = "numeric(18, 0)")]
    public decimal? CurrentZip { get; set; }

    [Column("CURRENT_GENDER")]
    [StringLength(20)]
    public string? CurrentGender { get; set; }

    [Column("CURRENT_PHONE_NUMBER")]
    [StringLength(15)]
    public string? CurrentPhoneNumber { get; set; }

    [Column("CURRENT_PERSONAL_EMAIL")]
    [StringLength(50)]
    public string? CurrentPersonalEmail { get; set; }

    [Column("CURRENT_MARITAL_STATUS")]
    [StringLength(50)]
    public string? CurrentMaritalStatus { get; set; }

    [Column("ETHNICITY")]
    [StringLength(10)]
    public string? Ethnicity { get; set; }

    [Column("SHAREHOLDER_STATUS")]
    public short? ShareholderStatus { get; set; }

    [Column("BENEFIT_PLAN_ID", TypeName = "numeric(18, 0)")]
    public decimal? BenefitPlanId { get; set; }

    [ForeignKey("BenefitPlanId")]
    [InverseProperty("Personals")]
    public virtual BenefitPlan? BenefitPlan { get; set; }

    [InverseProperty("Personal")]
    public virtual ICollection<Employment> Employments { get; set; } = new List<Employment>();
}
