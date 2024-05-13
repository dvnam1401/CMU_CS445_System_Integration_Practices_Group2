
using DashBoard.API.Models.Admin;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IAdminRepository
    {
        Task CreateAccountUserWithGroups(string firstName, string lastName, string userName, string email, string passwordHash, string phoneNumber, string department, bool isActive, int[] groupIds);
        Task CreateGroupAndAssignToUser(/*decimal userId,*/ string groupName, int[] permissionIds);
        Task ResetPassword(int userId, string newPassword);
        Task UpdateAccountUserWithGroups(int userId, string firstName, string lastName, string userName, string email,
            string passwordHash, string phoneNumber, string department, bool isActive, int[] groupIds);
        Task<List<GroupPermissionDto>> GetAllPermissionsForUser(int userId);
        //Task<List<GroupPermissionDto>> GetAllPermissionsByGroup();
        Task<Dictionary<string, List<GroupPermissionDto>>> GetAllPermissionsByGroup();
        Task DisablePermissionForUser(int userId, List<GroupPermissionDto> updates);
        Task<AccountUser> FindByEmailAsync(string email);
        Task<AccountUser> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(AccountUser identity, string password);
    }
}
