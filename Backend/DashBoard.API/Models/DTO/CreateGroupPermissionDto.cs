namespace DashBoard.API.Models.DTO
{
    public class CreateGroupPermissionDto
    {
        public string groupName { get; set; }
        public int[] permissionIds { get; set; }
    }
}
