
using DashBoard.API.Models.Admin;
using DashBoard.API.Models.DTO;
using System.Threading.Tasks;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IAdminRepository
    {
        Task CreateAccountUserWithGroups(string firstName, string lastName, string userName, string email, string passwordHash, string phoneNumber, string department, bool isActive, int[] groupIds);
        Task CreateGroupAndAssignToUser(/*decimal userId,*/ string groupName, int[] permissionIds);
        Task ResetPassword(int userId, string newPassword);
        Task UpdateAccountUserWithGroups(int userId, string firstName, string lastName, string userName, string email, string phoneNumber, string department, bool isActive, int[] groupIds);
        Task<Dictionary<string, List<GroupPermissionDto>>> GetAllPermissionsForUser(int userId);
        Task<List<GroupPermissionDto>> GetPermissionsForUserIsEnable(int userId);
        Task<IEnumerable<DetailAccountUserDto>> GetAllUser();
        Task<IEnumerable<Group>> GetAllGroup();
        Task<Dictionary<string, List<GroupPermissionDto>>> GetGroupPermissionById(int id);
        Task<Dictionary<string, List<GroupPermissionDto>>> GetPermissionsByUserAndGroup(int userId, int[] groupIds);
        Task<DetailAccountUserDto> GetUserById(int id);
        Task<Dictionary<string, List<GroupPermissionDto>>> GetAllPermissionsByGroup();
        Task DisablePermissionForUser(int userId, List<GroupPermissionDto> updates);
        Task DisablePermissionForGroup(string groupName, List<UpdatePermissionDto> updates);
        Task<AccountUser> FindByEmailAsync(string email);
        Task<AccountUser> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(AccountUser identity, string password);
        Task<ICollection<Permission>> GetAllPermissions();
        Task<List<AccountUser>> SearchUsersAsync(string searchTerm);
        Task DeleteAccountUserAsync(int userId);
    }
}
