using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var identityUser = await userManager.FindByEmailAsync(request.Email);
            if (identityUser is not null)
            {
                //check password
                var checkPassResult = await userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPassResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    //create a token and response
                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());
                    var response = new LoginResponseDto
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
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
