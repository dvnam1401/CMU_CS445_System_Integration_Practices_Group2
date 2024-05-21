namespace DashBoard.API.Models.DTO
{
    public class UpdatePermissionDto
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public bool? IsEnable { get; set; }
    }
}
