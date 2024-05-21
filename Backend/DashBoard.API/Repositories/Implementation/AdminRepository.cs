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
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.IdentityModel.Tokens;
using DashBoard.API.Models.Domain;

namespace DashBoard.API.Repositories.Implementation
{
    public class AdminRepository : IdentityDbContext, IAdminRepository
    {
        private readonly AdminContext? adminContext;
        public AdminRepository(AdminContext adminContext)
        {
            this.adminContext = adminContext;
        }

        private int GetAllIdAccount()
        {
            var result = adminContext.AccountUsers
                         .Select(e => e.UserId)
                         .Max(); // If Max returns null, replace with 0
            return result;
        }

        // tạo người dùng
        public async Task CreateAccountUserWithGroups(string firstName, string lastName, string userName, string email,
            string passwordHash, string phoneNumber, string department, bool isActive, int[] groupIds)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordHash);

            // Create a new AccountUser
            var accountUser = new AccountUser
            {
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
            await adminContext.SaveChangesAsync();
            var userId = accountUser.UserId;
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
                var groupPermissions = await adminContext.GroupPermissions
                    .Include(gp => gp.Permission)
                    .Include(gp => gp.Group)
                    .Where(gp => gp.GroupId == groupId)
                    .ToListAsync();
                foreach (var perm in groupPermissions)
                {
                    var userPermission = new UserPermission
                    {
                        UserId = userId,
                        GroupId = perm.GroupId,
                        GroupName = perm.Group.GroupName,
                        PermissionId = perm.PermissionId,
                        PermisisonName = perm.Permission.PermisstionName,
                        IsEnable = perm.IsEnable  // Inherits the enable status from group permission
                    };

                    // Add UserPermission to the DbContext
                    adminContext.UserPermissions.Add(userPermission);
                }
            }

            // Save changes to the database
            await adminContext.SaveChangesAsync();
        }

        // tạo group
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
            string phoneNumber, string department, bool isActive, int[] groupIds)
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
            accountUser.PhoneNumber = phoneNumber;
            accountUser.Department = department;
            accountUser.IsActive = isActive;

            if (groupIds != null && groupIds.Length > 0)
            {// Update groups
             // First, remove old groups
                var existingGroups = adminContext.UserGroups.Where(ug => ug.UserId == userId).ToList();
                adminContext.UserGroups.RemoveRange(existingGroups);
                //await adminContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT UserGroups OFF");
                var existingPermissions = adminContext.UserPermissions.Where(up => up.UserId == userId).ToList();
                adminContext.UserPermissions.RemoveRange(existingPermissions);
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
                    // Add or update permissions associated with the group
                    var groupPermissions = adminContext.GroupPermissions
                        .Include(gp => gp.Permission)
                        .Include(gp => gp.Group)
                        .Where(gp => gp.GroupId == groupId).ToList();
                    foreach (var perm in groupPermissions)
                    {
                        var userPermission = new UserPermission
                        {
                            UserId = userId,
                            GroupId = groupId,
                            GroupName = perm.Group.GroupName,
                            PermissionId = perm.PermissionId,
                            PermisisonName = perm.Permission.PermisstionName,
                            IsEnable = perm.IsEnable  // Inherits the enable status from group permission
                        };
                        adminContext.UserPermissions.Add(userPermission);
                    }
                }
            }
            // Save changes to the database
            await adminContext.SaveChangesAsync();
        }

        // lấy tất cả quyền người dùng theo userId không tính isEnable true or false
        public async Task<Dictionary<string, List<GroupPermissionDto>>> GetAllPermissionsForUser(int userId)
        {
            // Lấy tất cả UserPermission liên quan đến userId và bao gồm thông tin về Permission
            var userPermissions = await adminContext.UserPermissions
                .Where(up => up.UserId == userId)
                //.Include(up => up.Permission)
                .ToListAsync();

            Dictionary<string, List<GroupPermissionDto>> permissions = new Dictionary<string, List<GroupPermissionDto>>();

            // Duyệt qua từng UserPermission để xây dựng dictionary
            foreach (var up in userPermissions)
            {
                var groupPermission = await adminContext.GroupPermissions
                    .FirstOrDefaultAsync(gp => gp.GroupId == up.GroupId && gp.PermissionId == up.PermissionId && gp.IsEnable == true);
                if (groupPermission is not null)
                {
                    // Tạo đối tượng GroupPermissionDto từ UserPermission
                    var permissionDto = new GroupPermissionDto
                    {
                        GroupName = up.GroupName,
                        PermissionId = up.PermissionId,
                        PermissionName = up.PermisisonName,
                        IsEnable = up.IsEnable
                    };

                    // Thêm vào dictionary, tạo nhóm nếu nó chưa tồn tại
                    if (!permissions.ContainsKey(permissionDto.GroupName))
                    {
                        permissions[permissionDto.GroupName] = new List<GroupPermissionDto>();
                    }
                    permissions[permissionDto.GroupName].Add(permissionDto);
                }
            }

            return permissions;            
        }

        // lấy ds quyền người dùng được cho phép theo userId
        // dùng viết chức năng login
        public async Task<List<string>> GetPermissionsForUserIsEnable(int userId)
        {
            var roles = await adminContext.UserPermissions
                        .Where(ug => ug.UserId == userId && ug.IsEnable == true)
                        .Select(ug => ug.PermisisonName)
                        .Distinct()
                        .ToListAsync();

            //var permissionsInfo = await adminContext.GroupPermissions
            //                    .Where(gp => userGroups.Contains(gp.GroupId) && gp.IsEnable == true)
            //                    .Select(gp => new GroupPermissionDto
            //                    {
            //                        GroupName = gp.Group.GroupName,
            //                        PermissionId = gp.PermissionId,
            //                        PermissionName = gp.Permission.PermisstionName,
            //                        IsEnable = gp.IsEnable
            //                    })
            //                    .Distinct()
            //                    .ToListAsync();

            //// Loại bỏ các quyền trùng lặp
            //var distinctPermissionsInfo = permissionsInfo
            //    .GroupBy(p => new { p.GroupName, p.PermissionId, p.PermissionName, p.IsEnable })
            //    .Select(g => g.First())
            //    .ToList();

            return roles;
        }

        // lấy tất cả ds quyền trong group
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
                        IsEnable = gp.IsEnable,
                        //Users = adminContext.UserGroups
                        //.Where(ug => ug.GroupId == gp.GroupId)
                        //.Select(ug => new GroupUserDto
                        //{
                        //    UserName = ug.User.UserName,
                        //    UserEmail = ug.User.Email,
                        //}).ToList()
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

        // lấy ra ds các quyền trong group theo groupId
        public async Task<Dictionary<string, List<GroupPermissionDto>>> GetGroupPermissionById(int id)
        {
            var group = await adminContext.Groups
                     .Where(g => g.GroupId == id /*&& ug.IsEnable == true*/)
                     .Select(ug => new
                     {
                         ug.GroupId,
                         ug.GroupName
                     }).ToListAsync();

            Dictionary<string, List<GroupPermissionDto>> permissions = new Dictionary<string, List<GroupPermissionDto>>();
            foreach (var groupId in group)
            {
                var permissionsInfo = await adminContext.GroupPermissions
                                    .Where(gp => groupId.GroupId == gp.GroupId) /*&& gp.IsEnable == true*/
                                    .Select(gp => new GroupPermissionDto
                                    {
                                        GroupName = gp.Group.GroupName,
                                        PermissionId = gp.PermissionId,
                                        PermissionName = gp.Permission.PermisstionName,
                                        IsEnable = gp.IsEnable
                                    })
                                    .Distinct()
                                    .ToListAsync();

                permissions[groupId.GroupName] = new List<GroupPermissionDto>();
                permissions[groupId.GroupName].AddRange(permissionsInfo);
            }
            return permissions;
        }

        // lấy ra ds các quyền đã có trong user, và lấy các quyền chưa được cập nhật
        public async Task<Dictionary<string, List<GroupPermissionDto>>> GetPermissionsByUserAndGroup(int userId, int[] groupIds)
        {
            Dictionary<string, List<GroupPermissionDto>> permissions = new Dictionary<string, List<GroupPermissionDto>>();

            foreach (int groupId in groupIds)
            {
                var group = await adminContext.Groups
                    .Where(g => g.GroupId == groupId)
                    .FirstOrDefaultAsync();

                if (group == null)
                {
                    continue;  // Skip this iteration if the group is null
                }

                //var groupPermissions = await adminContext.GroupPermissions
                //    .Include(gp => gp.Permission)
                //    .Where(gp => gp.GroupId == groupId/* && gp.IsEnable == true*/)
                //    .ToListAsync();
                var groupPermissions = await adminContext.UserPermissions
                    .Where(gp => gp.GroupId == groupId/* && gp.IsEnable == true*/)
                    .ToListAsync();

                if (!groupPermissions.Any())
                {
                    var fallbackPermissions = await GetGroupPermissionById(groupId);
                    permissions[group.GroupName] = fallbackPermissions.GetValueOrDefault(group.GroupName, new List<GroupPermissionDto>());
                    continue;
                }

                List<GroupPermissionDto> permissionDtos = new List<GroupPermissionDto>();
                foreach (var gp in groupPermissions)
                {
                    if (gp == null || gp.PermisisonName == null) continue;  // Check for null before access

                    permissionDtos.Add(new GroupPermissionDto
                    {
                        GroupName = group.GroupName,
                        PermissionId = gp.PermissionId,
                        PermissionName = gp.PermisisonName,
                        IsEnable = gp.IsEnable
                    });
                }

                if (permissionDtos.Any())
                {
                    permissions[group.GroupName] = permissionDtos;
                }
            }

            return permissions;
        }

        // lấy tất cả group
        public async Task<IEnumerable<Group>> GetAllGroup()
        {
            return await adminContext.Groups.ToListAsync();
        }

        // Tắt các quyền cụ thể của người dùng trong bảng UserPermission
        public async Task DisablePermissionForUser(int userId, List<GroupPermissionDto> updates)
        {
            var userPermissions = await adminContext.UserPermissions
                .Where(up => up.UserId == userId)
                .ToListAsync();

            var permissionsToUpdate = new List<UserPermission>();

            foreach (var update in updates)
            {
                // Tìm kiếm các quyền hiện có dựa trên thông tin từ update
                var matchingPermissions = userPermissions
                    .Where(up => up.PermissionId == update.PermissionId && up.PermisisonName == update.PermissionName && up.GroupName == update.GroupName)
                    .ToList();

                // Cập nhật trạng thái IsEnable nếu khác với trạng thái mới
                foreach (var perm in matchingPermissions)
                {
                    if (perm.IsEnable != update.IsEnable)
                    {
                        perm.IsEnable = update.IsEnable;
                        permissionsToUpdate.Add(perm);
                    }
                }
            }

            // Thực hiện cập nhật hàng loạt nếu có thay đổi
            if (permissionsToUpdate.Any())
            {
                adminContext.UserPermissions.UpdateRange(permissionsToUpdate);
                await adminContext.SaveChangesAsync();
            }
            //var userGroupsWithGroupInfo = await adminContext.UserGroups
            //    .Where(ug => ug.UserId == userId)
            //    .Include(ug => ug.Group)
            //    .ToListAsync();
            //var filteredPermissions = new List<GroupPermission>();

            //foreach (var update in updates)
            //{
            //    // Lấy tất cả các permissions hiện có, không quan tâm đến trạng thái IsEnable hiện tại
            //    var allPermissions = userGroupsWithGroupInfo
            //        .Where(ug => ug.Group.GroupName == update.GroupName)
            //        .SelectMany(ug => adminContext.GroupPermissions
            //            .Where(gp => gp.GroupId == ug.GroupId && gp.PermissionId == update.PermissionId))
            //        .ToList();

            //    // Bây giờ, chúng ta chỉ xét các quyền cần được cập nhật
            //    var permissionsToUpdate = allPermissions
            //        .Where(gp => gp.IsEnable != update.IsEnable)
            //        .ToList();

            //    // Chuẩn bị tất cả các quyền cần cập nhật
            //    foreach (var perm in permissionsToUpdate)
            //    {
            //        perm.IsEnable = update.IsEnable;
            //        filteredPermissions.Add(perm);
            //    }
            //}

            //// Thực hiện cập nhật hàng loạt
            //if (filteredPermissions.Any())
            //{
            //    adminContext.GroupPermissions.UpdateRange(filteredPermissions);
            //    await adminContext.SaveChangesAsync();
            //}
        }

        // tìm kiểm người dùng theo email
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

        // tìm kiểm người dùng theo username
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

        // kiểm tra passwrod
        public async Task<bool> CheckPasswordAsync(AccountUser identity, string password)
        {
            // Kiểm tra mật khẩu nhập vào có khớp với mật khẩu đã được mã hóa không
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, identity.PasswordHash);
            return isValidPassword;
        }

        // lấy tất cả người dùng
        public async Task<IEnumerable<DetailAccountUserDto>> GetAllUser()
        {
            var user = await adminContext.AccountUsers
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .Select(u => new DetailAccountUserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FullName = u.FirstName + " " + u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    IsActive = u.IsActive,
                    Department = u.Department,
                    UserNameGroups = u.UserGroups.Where(ur => ur.IsEnable == true).Select(ug => ug.Group.GroupName).ToList(),
                }).ToListAsync();

            return user;
        }

        // lấy người dùng theo userId
        public async Task<DetailAccountUserDto> GetUserById(int id)
        {
            var user = await adminContext.AccountUsers
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .Where(u => u.UserId == id)
                .Select(u => new DetailAccountUserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    IsActive = u.IsActive,
                    Department = u.Department,
                    Groups = u.UserGroups.Where(ug => ug.IsEnable == true).Select(ug => new Group
                    {
                        GroupId = ug.Group.GroupId,
                        GroupName = ug.Group.GroupName,
                    }).ToList(),
                }).FirstOrDefaultAsync(u => u.UserId == id);

            return user;
        }

        public async Task<ICollection<Permission>> GetAllPermissions()
        {
            return await adminContext.Permissions.ToListAsync();
        }

        public async Task DisablePermissionForGroup(string groupName, List<UpdatePermissionDto> updates)
        {
            var groupPermissions = await adminContext.GroupPermissions
         .Where(gp => gp.Group.GroupName == groupName && updates.Select(u => u.PermissionId).Contains(gp.PermissionId))
         .ToListAsync();

            if (!groupPermissions.Any())
            {
                throw new InvalidOperationException("No permissions found for the provided group.");
            }

            foreach (var update in updates)
            {
                var permissionToUpdate = groupPermissions.FirstOrDefault(gp => gp.PermissionId == update.PermissionId);
                if (permissionToUpdate != null)
                {
                    permissionToUpdate.IsEnable = update.IsEnable;
                }
            }

            await adminContext.SaveChangesAsync();
        }

        // tìm kiếm gần đúng User Name, Email, Phone Number, Department, First Name và Last Name
        public async Task<List<AccountUser>> SearchUsersAsync(string searchTerm)
        {
            var normalizedSearchTerm = searchTerm?.ToLower() ?? string.Empty;
            var users = await adminContext.AccountUsers
                .Where(u => u.UserName.ToLower().Contains(normalizedSearchTerm)
                         || u.Email.ToLower().Contains(normalizedSearchTerm)
                         || (u.PhoneNumber != null && u.PhoneNumber.ToLower().Contains(normalizedSearchTerm))
                         || (u.Department != null && u.Department.ToLower().Contains(normalizedSearchTerm))
                         || u.FirstName.ToLower().Contains(normalizedSearchTerm)
                         || u.LastName.ToLower().Contains(normalizedSearchTerm)
                         || (u.FirstName.ToLower() + " " + u.LastName.ToLower()).Contains(normalizedSearchTerm))
                .ToListAsync();
            return users;
        }
        public async Task DeleteAccountUserAsync(int userId)
        {
            // Xóa các UserPermission liên quan
            var userPermissions = await adminContext.UserPermissions
                .Where(up => up.UserId == userId)
                .ToListAsync();
            adminContext.UserPermissions.RemoveRange(userPermissions);

            // Xóa các UserGroup liên quan
            var userGroups = await adminContext.UserGroups
                .Where(ug => ug.UserId == userId)
                .ToListAsync();
            adminContext.UserGroups.RemoveRange(userGroups);

            // Cuối cùng, xóa AccountUser
            var accountUser = await adminContext.AccountUsers
                .FindAsync(userId);
            if (accountUser != null)
            {
                adminContext.AccountUsers.Remove(accountUser);
            }

            // Lưu thay đổi
            await adminContext.SaveChangesAsync();
        }
    }
}
