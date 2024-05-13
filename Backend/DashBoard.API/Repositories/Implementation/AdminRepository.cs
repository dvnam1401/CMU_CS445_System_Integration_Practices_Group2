using DashBoard.API.Data;
using DashBoard.API.Models.Admin;
using Group = DashBoard.API.Models.Admin.Group;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Text.RegularExpressions;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Implementation
{
    public class AdminRepository : IdentityDbContext, IAdminRepository
    {
        private readonly AdminContext? adminContext;
        public AdminRepository(AdminContext adminContext)
        {
            this.adminContext = adminContext;
        }
        public async Task CreateAccountUserWithGroups(string firstName, string lastName, string userName, string email,
            string passwordHash, string phoneNumber, string department, bool isActive, int[] groupIds)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordHash);

            // Create a new AccountUser
            var accountUser = new AccountUser
            {
                //UserId = 1,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                PasswordHash = hashedPassword,
                PhoneNumber = phoneNumber,
                Department = department,
                IsActive = isActive
            };

            // Add the AccountUser to the DbContext
            adminContext.AccountUsers.Add(accountUser);

            // Assign groups to the user
            foreach (var groupId in groupIds)
            {
                var group = await adminContext.Groups.FindAsync(groupId);
                if (group != null)
                {
                    // Create a new UserGroup
                    var userGroup = new UserGroup
                    {
                        User = accountUser,
                        Group = group,
                        IsEnable = true  // or set based on your business rules
                    };

                    // Add the UserGroup to the DbContext   
                    adminContext.UserGroups.Add(userGroup);
                }
            }

            // Save changes to the database
            await adminContext.SaveChangesAsync();
        }

        // Method to create a group, add permissions, and assign it to a user
        public async Task CreateGroupAndAssignToUser(/*decimal userId,*/ string groupName, int[] permissionIds)
        {
            await adminContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Groups ON");
            // Create a new group
            var group = new Group
            {
                //  GroupId = 5,
                GroupName = groupName
            };
            adminContext.Groups.Add(group);
            await adminContext.SaveChangesAsync(); // Saving to get GroupId

            // Disable IDENTITY_INSERT for 'Groups' table after insertion
            await adminContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Groups OFF");

            // Add permissions to the group
            foreach (var permissionId in permissionIds)
            {
                var permission = await adminContext.Permissions.FindAsync(permissionId);
                if (permission != null)
                {
                    var groupPermission = new GroupPermission
                    {
                        GroupId = group.GroupId,
                        PermissionId = permissionId,
                        IsEnable = true
                    };
                    adminContext.GroupPermissions.Add(groupPermission);
                }
            }

            await adminContext.SaveChangesAsync();
        }

        // reset password
        public async Task ResetPassword(int userId, string newPassword)
        {
            var user = await adminContext.AccountUsers.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await adminContext.SaveChangesAsync();
        }

        // Edit Account User
        public async Task UpdateAccountUserWithGroups(int userId, string firstName, string lastName, string userName, string email,
            string passwordHash, string phoneNumber, string department, bool isActive, int[] groupIds)
        {
            var accountUser = await adminContext.AccountUsers.FindAsync(userId);
            if (accountUser == null)
            {
                throw new ArgumentException("User not found.");
            }

            // Update the AccountUser's details
            accountUser.FirstName = firstName;
            accountUser.LastName = lastName;
            accountUser.UserName = userName;
            accountUser.Email = email;
            if (!string.IsNullOrEmpty(passwordHash))
            {
                accountUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordHash);
            }
            accountUser.PhoneNumber = phoneNumber;
            accountUser.Department = department;
            accountUser.IsActive = isActive;

            // Update groups
            // First, remove old groups
            var existingGroups = adminContext.UserGroups.Where(ug => ug.UserId == userId).ToList();
            adminContext.UserGroups.RemoveRange(existingGroups);
            //await adminContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT UserGroups OFF");
            // Add new groups
            foreach (var groupId in groupIds)
            {
                var group = await adminContext.Groups.FindAsync(groupId);
                if (group != null)
                {
                    var userGroup = new UserGroup
                    {
                        UserId = userId,
                        GroupId = groupId,
                        IsEnable = true  // or set based on your business rules
                    };
                    adminContext.UserGroups.Add(userGroup);
                }
            }

            // Save changes to the database
            await adminContext.SaveChangesAsync();
        }

        public async Task<List<GroupPermissionDto>> GetAllPermissionsForUser(int userId)
        {
            var userGroups = await adminContext.UserGroups
                        .Where(ug => ug.UserId == userId /*&& ug.IsEnable == true*/)
                        .Select(ug => ug.GroupId)
                        .ToListAsync();

            var permissionsInfo = await adminContext.GroupPermissions
                                .Where(gp => userGroups.Contains(gp.GroupId) /*&& gp.IsEnable == true*/)
                                .Select(gp => new GroupPermissionDto
                                {
                                    GroupName = gp.Group.GroupName,
                                    PermissionId = gp.PermissionId,
                                    PermissionName = gp.Permission.PermisstionName,
                                    IsEnable = gp.IsEnable
                                })
                                .Distinct()
                                .ToListAsync();

            return permissionsInfo;
        }
        //public async Task<List<GroupPermissionDto>> GetAllPermissionsByGroup()
        //{
        //    var groups = await adminContext.Groups.ToListAsync();

        //    List<GroupPermissionDto> permissionsInfo = new List<GroupPermissionDto>();

        //    foreach (var group in groups)
        //    {
        //        var groupPermissions = await adminContext.GroupPermissions
        //            .Where(gp => gp.GroupId == group.GroupId)
        //            .Select(gp => new GroupPermissionDto
        //            {
        //                GroupName = group.GroupName,
        //                PermissionId = gp.PermissionId,
        //                PermissionName = gp.Permission.PermisstionName,
        //                IsEnable = gp.IsEnable
        //            })
        //            .Distinct()
        //            .ToListAsync();

        //        permissionsInfo.AddRange(groupPermissions);
        //    }

        //    return permissionsInfo;
        //}

        public async Task<Dictionary<string, List<GroupPermissionDto>>> GetAllPermissionsByGroup()
        {
            var groups = await adminContext.Groups.ToListAsync();

            Dictionary<string, List<GroupPermissionDto>> groupedPermissions = new Dictionary<string, List<GroupPermissionDto>>();

            foreach (var group in groups)
            {
                var groupPermissions = await adminContext.GroupPermissions
                    .Where(gp => gp.GroupId == group.GroupId)
                    .Select(gp => new GroupPermissionDto
                    {
                        GroupName = group.GroupName,
                        PermissionName = gp.Permission.PermisstionName,
                        PermissionId = gp.PermissionId,
                        IsEnable = gp.IsEnable
                    })
                    .ToListAsync();

                if (groupedPermissions.ContainsKey(group.GroupName))
                {
                    groupedPermissions[group.GroupName].AddRange(groupPermissions);
                }
                else
                {
                    groupedPermissions.Add(group.GroupName, groupPermissions);
                }
            }

            return groupedPermissions;
        }

        public async Task DisablePermissionForUser(int userId, List<GroupPermissionDto> updates)
        {
            var userGroupsWithGroupInfo = await adminContext.UserGroups
                .Where(ug => ug.UserId == userId)
                .Include(ug => ug.Group)
                .ToListAsync();
            var filteredPermissions = new List<GroupPermission>();

            foreach (var update in updates)
            {
                // Lấy tất cả các permissions hiện có, không quan tâm đến trạng thái IsEnable hiện tại
                var allPermissions = userGroupsWithGroupInfo
                    .Where(ug => ug.Group.GroupName == update.GroupName)
                    .SelectMany(ug => adminContext.GroupPermissions
                        .Where(gp => gp.GroupId == ug.GroupId && gp.PermissionId == update.PermissionId))
                    .ToList();

                // Bây giờ, chúng ta chỉ xét các quyền cần được cập nhật
                var permissionsToUpdate = allPermissions
                    .Where(gp => gp.IsEnable != update.IsEnable)
                    .ToList();

                if (!permissionsToUpdate.Any())
                {
                    throw new InvalidOperationException($"No permissions found to update for GroupName '{update.GroupName}' and PermissionId {update.PermissionId}.");
                }

                // Chuẩn bị tất cả các quyền cần cập nhật
                foreach (var perm in permissionsToUpdate)
                {
                    perm.IsEnable = update.IsEnable;
                    filteredPermissions.Add(perm);
                }
            }

            // Thực hiện cập nhật hàng loạt
            if (filteredPermissions.Any())
            {
                adminContext.GroupPermissions.UpdateRange(filteredPermissions);
                await adminContext.SaveChangesAsync();
            }
        }
        public async Task<AccountUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty", nameof(email));
            }

            var user = await adminContext.AccountUsers
                                         .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            return user;
        }

        public async Task<AccountUser> FindByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("Username cannot be empty", nameof(userName));
            }

            var user = await adminContext.AccountUsers
                                         .FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());

            return user;
        }

        public async Task<bool> CheckPasswordAsync(AccountUser identity, string password)
        {
            // Kiểm tra mật khẩu nhập vào có khớp với mật khẩu đã được mã hóa không
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, identity.PasswordHash);
            return isValidPassword;
        }
    }
}
