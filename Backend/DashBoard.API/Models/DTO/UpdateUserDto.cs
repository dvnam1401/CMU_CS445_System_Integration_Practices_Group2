namespace DashBoard.API.Models.DTO
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Chỉ nhận mật khẩu mới nếu người dùng muốn cập nhật
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public int[] GroupIds { get; set; }
    }
}
