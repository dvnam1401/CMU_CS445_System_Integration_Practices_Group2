using Microsoft.AspNetCore.Identity;

namespace DashBoard.API.Repositories.Inteface
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
