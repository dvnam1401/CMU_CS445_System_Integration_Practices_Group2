namespace DashBoard.API.Models.DTO
{
    public class GroupPermissionDto
    {
        public string GroupName { get; set; }
        public string PermissionName { get; set; }
        public int PermissionId { get; set; }
        public bool? IsEnable { get; set; }
        public List<GroupUserDto>? Users { get; set; }
    }
}
