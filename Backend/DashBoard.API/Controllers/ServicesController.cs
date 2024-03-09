using DashBoard.API.Models;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository serviceRepository;
        public ServicesController(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existing = await serviceRepository.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            return Ok(existing);
        }

        [HttpGet("GetAllBenifit")]
        public async Task<IActionResult> GetAll()
        {
            var result = await serviceRepository.GetByAll();
            return Ok(result);
        }

        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var result = await serviceRepository.GetByAllEmployee();
            return Ok(result);
        }

    }
}
