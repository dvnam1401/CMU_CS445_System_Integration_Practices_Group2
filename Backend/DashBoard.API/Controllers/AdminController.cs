using DashBoard.API.Models.Admin;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System.Text.RegularExpressions;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IAdminRepository authRepository;

        public AdminController(IAdminRepository authRepository,
            ITokenRepository tokenRepository,        
            UserManager<IdentityUser> userManager
            )
        {
            this.authRepository = authRepository;
            this.tokenRepository = tokenRepository;        
        }

        [HttpPost("CreateAccountUser")]
        public async Task<IActionResult> CreateAccountUser([FromBody] CreateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await authRepository.CreateAccountUserWithGroups(
                    model.FirstName,
                    model.LastName,
                    model.UserName,
                    model.Email,
                    model.Password,
                    model.PhoneNumber,
                    model.Department,
                    model.IsActive,
                    model.GroupIds
                );

                return Ok("User created successfully with groups.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateGroupPermission")]
        public async Task<IActionResult> CreateGroupPermission([FromBody] CreateGroupPermissionDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await authRepository.CreateGroupAndAssignToUser(
                    model.groupName,
                    model.permissionIds
                    );
                return Ok("Created group permission successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await authRepository.ResetPassword(model.UserId, model.NewPassword);
                return Ok("Password reset successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await authRepository.UpdateAccountUserWithGroups(
                    model.UserId,
                    model.FirstName,
                    model.LastName,
                    model.UserName,
                    model.Email,
                    model.Password,
                    model.PhoneNumber,
                    model.Department,
                    model.IsActive,
                    model.GroupIds
                );

                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                // Cân nhắc thêm các loại exception cụ thể và xử lý chúng một cách thích hợp
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-permissions")]
        public async Task<IActionResult> GetPermissionsForUser(int userId)
        {
            try
            {
                var permissions = await authRepository.GetAllPermissionsForUser(userId);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-all-permissions-by-group")]
        public async Task<IActionResult> GetAllPermissionsByGroup()
        {
            try
            {
                var permissions = await authRepository.GetAllPermissionsByGroup();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update-permissions-status")]
        public async Task<IActionResult> UpdatePermissionsStatus(int userId, [FromBody] List<GroupPermissionDto> updates)
        {
            try
            {
                await authRepository.DisablePermissionForUser(userId, updates);
                return Ok("Permissions statuses updated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            // Regex pattern to check if identifier is an email
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var match = Regex.Match(loginDto.Email, emailPattern);
            AccountUser? identityUser = null;
            if (match.Success)
            {
                identityUser = await authRepository.FindByEmailAsync(loginDto.Email);
            }
            else
            {
                identityUser = await authRepository.FindByUserNameAsync(loginDto.Email);
            }
            if (identityUser is not null)
            {
                var checkPassResult = await authRepository.CheckPasswordAsync(identityUser, loginDto.Password);
                if (checkPassResult)
                {
                    var permissions = await authRepository.GetAllPermissionsForUser(identityUser.UserId);
                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, permissions);
                    var response = new LoginResponseDto
                    {
                        Email = loginDto.Email,
                        Roles = permissions.Select(p => p.PermissionName).ToList(),
                        Token = jwtToken,
                    };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or password incorrect");
            return ValidationProblem(ModelState);
        }   

    }
}
