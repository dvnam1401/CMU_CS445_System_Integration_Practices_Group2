namespace DashBoard.API.Models.DTO
{
    public class VacationMysqlDto
    {
        public uint EmployeeNumber { get; set; }
        public DateTime? Dayoff { get; set; }
        public string? ResignationContent { get; set; }
    }
}
