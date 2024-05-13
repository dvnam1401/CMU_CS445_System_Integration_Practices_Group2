using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatedController : ControllerBase
    {
        private readonly ICreatedRepository createdRepository;
        public CreatedController(ICreatedRepository createdRepository)
        {
            this.createdRepository = createdRepository;
        }

        [HttpPost("createPersonal")]
        public async Task<IActionResult> CreatePersoanal([FromBody] CreatePersonalDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // This will return a 400 Bad Request if validation fails
            }
            await createdRepository.CreatePersonalAsync(request);
            return Ok(request);
        }

        [HttpPost("createEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // This will return a 400 Bad Request if validation fails
            }
            await createdRepository.CreateEmployeeAsync(request);
            return Ok(request);
        }
    }
}
