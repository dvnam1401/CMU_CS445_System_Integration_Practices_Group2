﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DashBoard.API.Models.Domain;

namespace DashBoard.API.Repositories.Implementation
{
    public class PersonalDetailDto
    {
        public decimal PersonalId { get; set; }
        public string? CurrentFirstName { get; set; }
        public string? CurrentLastName { get; set; }
        public string? CurrentMiddleName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? CurrentCity { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public string? CurrentAddress1 { get; set; }
        public string? DriversLicense { get; set; }
        public decimal? CurrentZip { get; set; }
        public string? CurrentMaritalStatus { get; set; }
        public short? ShareholderStatus { get; set; }
        public string BenefitPlanName { get; set; }
        public string? CurrentCountry { get; set; }
        public string? CurrentAddress2 { get; set; }
        public string? CurrentGender { get; set; }
        public string? CurrentPhoneNumber { get; set; }
        public string? CurrentPersonalEmail { get; set; }
        public string? Ethnicity { get; set; }
    }
}
