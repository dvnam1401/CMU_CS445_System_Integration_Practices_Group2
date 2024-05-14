using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private readonly IDetailRepository detailRepository;
        public DetailController(IDetailRepository detailRepository)
        {
            this.detailRepository = detailRepository;
        }

        [HttpGet("getPersonal-by-id/{id}")]
        public async Task<ActionResult<PersonalDetailDto>> GetPersonalById(decimal id)
        {
            try
            {
                var personal = await detailRepository.GetPersonalByIdAsync(id);
                if (personal == null)
                {
                    return NotFound();
                }
                return Ok(personal);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getAll-personal")]
        public async Task<ActionResult<PersonalDetailDto>> GetAllPersonal()
        {
            try
            {
                var personal = await detailRepository.GetAllPersonalAsync();
                if (personal == null)
                {
                    return NotFound();
                }
                return Ok(personal);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("get-employee-by-id/{id}")]
        public async Task<ActionResult<EditEmployeeDto>> GetEmployee(int id)
        {
            try
            {
                var employeeDto = await detailRepository.GetEmployeeByIdAsync(id);
                return Ok(employeeDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAll-employee")]
        public async Task<ActionResult<PersonalDetailDto>> GetAllEmployee()
        {
            try
            {
                var personal = await detailRepository.GetAllEmployeeAsync();
                if (personal == null)
                {
                    return NotFound();
                }
                return Ok(personal);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
