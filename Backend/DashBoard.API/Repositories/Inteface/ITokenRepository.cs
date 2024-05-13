using DashBoard.API.Models.Admin;
using DashBoard.API.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace DashBoard.API.Repositories.Inteface
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(AccountUser user, List<GroupPermissionDto> roles);
    }
}
