namespace DashBoard.API.Models.DTO
{
    public class EmployeeFilterDto
    {
        public bool? IsAscending { get; set; }
        public bool? Gender { get; set; }
        public string? PayRateName { get; set; } // category: partime or fulltime
        public string? Ethnicity { get; set; }
        public string? Department { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
    }
}
