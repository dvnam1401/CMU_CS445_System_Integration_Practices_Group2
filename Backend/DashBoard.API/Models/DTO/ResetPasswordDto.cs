using System.ComponentModel.DataAnnotations;

namespace DashBoard.API.Models.DTO
{
    public class ResetPasswordDto
    {
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
    }
}
