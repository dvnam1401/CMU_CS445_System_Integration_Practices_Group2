using Azure;
using DashBoard.API.Models.Admin;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
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

        [HttpPost("create-AccountUser")]
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

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("create-GroupPermission")]
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
                return Ok();
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
                return Ok();
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
                    var permissions = await authRepository.GetPermissionsForUserIsEnable(identityUser.UserId);
                    //var distinctPermissions = permissions
                    //    .Select(p => p.PermissionName)
                    //    .Distinct()  // Sử dụng Distinct để loại bỏ các quyền trùng lặp
                    //    .ToList();
                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, permissions);
                    var response = new LoginResponseDto
                    {
                        Email = loginDto.Email,
                        //Roles = permissions.Select(p => p.PermissionName).ToList(),
                        Roles = permissions,
                        Token = jwtToken,
                    };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or password incorrect");
            return ValidationProblem(ModelState);
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
                    model.PhoneNumber,
                    model.Department,
                    model.IsActive,
                    model.GroupIds
                );

                return Ok(/*"User updated successfully."*/);
            }
            catch (Exception ex)
            {
                // Cân nhắc thêm các loại exception cụ thể và xử lý chúng một cách thích hợp
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update-permissions-status")]
        public async Task<IActionResult> UpdatePermissionsStatus(int userId, [FromBody] List<GroupPermissionDto> updates)
        {
            try
            {
                await authRepository.DisablePermissionForUser(userId, updates);
                return Ok(/*"Permissions statuses updated successfully."*/);
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

        [HttpPut("update-permission")]
        public async Task<IActionResult> UpdatePermissionGroup(string groupName, [FromBody] List<UpdatePermissionDto> updates)
        {
            try
            {
                await authRepository.DisablePermissionForGroup(groupName, updates);
                return Ok();
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

        [HttpGet("get-permissions-userId")]
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

        [HttpGet("getAll-permission")]
        public async Task<IActionResult> GetAllPermission()
        {
            try
            {
                var result = await authRepository.GetAllPermissions();
                return Ok(result);
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

        [HttpGet("get-group-by-id")]
        public async Task<IActionResult> GetGroupPermissionById(int id)
        {
            try
            {
                var response = await authRepository.GetGroupPermissionById(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("get-group-user-by-id")]
        public async Task<IActionResult> GetPermissionUserById(int userId, int[] groupId)
        {
            try
            {
                var response = await authRepository.GetPermissionsByUserAndGroup(userId, groupId);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getAll-group")]
        public async Task<IActionResult> GetAllGroup()
        {
            try
            {
                var response = await authRepository.GetAllGroup();
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("getAll-user")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var response = await authRepository.GetAllUser();
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getUser-by-id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var response = await authRepository.GetUserById(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("find-account-user")]
        public async Task<IActionResult> FindAccountUser(string search)
        {
            try
            {
                List<DetailAccountUserDto> response = new List<DetailAccountUserDto>();
                var resutl = await authRepository.SearchUsersAsync(search);
                foreach (var u in resutl)
                {
                    response.Add(new DetailAccountUserDto
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        FullName = u.FirstName + " " + u.LastName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        IsActive = u.IsActive,
                        Department = u.Department,
                        UserNameGroups = u.UserGroups.Where(ur => ur.IsEnable == true).Select(ug => ug.Group.GroupName).ToList(),
                    });
                    ;
                }
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(int userId)
        {
            try
            {
                await authRepository.DeleteAccountUserAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ví dụ: log lỗi, thông báo cho người dùng, v.v.)
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

