﻿namespace DashBoard.API.Models.DTO
{
    public class EmployeeFilterDto
    {
        public bool? IsAscending { get; set; }
        public string? Gender { get; set; }
        public string? Category { get; set; } // category: partime or fulltime
        public string? Ethnicity { get; set; }
        public string? Department { get; set; } // phong ban
        public int? Year { get; set; }
        public int? Month { get; set; }
        public short? ShareholderStatus { get; set; }
    }
}
