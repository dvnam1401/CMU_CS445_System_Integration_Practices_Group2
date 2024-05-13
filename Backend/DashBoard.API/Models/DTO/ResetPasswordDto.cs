using System.ComponentModel.DataAnnotations;

namespace DashBoard.API.Models.DTO
{
    public class ResetPasswordDto
    {
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required]
        public string? Token { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
