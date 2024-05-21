using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DashBoard.API.Models.Admin;

namespace DashBoard.API.Models.DTO
{
    public class DetailAccountUserDto
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? Department { get; set; }
        public List<string> UserNameGroups { get; set; } = new List<string>();
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
