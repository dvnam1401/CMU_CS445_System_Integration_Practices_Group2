namespace DashBoard.API.Models.DTO
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public int[] GroupIds { get; set; }
    }
}
