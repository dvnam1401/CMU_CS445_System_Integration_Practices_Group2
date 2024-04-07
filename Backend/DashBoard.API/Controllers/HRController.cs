using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "HR")]
    public class HRController : ControllerBase
    {
        private readonly IHRRepository hRRepository;

        public HRController(IHRRepository hRRepository)
        {
            this.hRRepository = hRRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployeeById(uint id)
        {
            var response = await hRRepository.FindEmployee(id);
            if (response is not null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto request)
        {
            await hRRepository.CreateEployeeAsync(request);
            return Ok(request);
        }

        [HttpPut]
        [Route("{idEmployee}")]
        public async Task<IActionResult> EditEmployee([FromRoute] int idEmployee, HRUpdateEmployeeDto request)
        {
            //map dto to domain model
            var response = new HRUpdateEmployeeDto
            {
                EmployeeId = idEmployee,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Dateofbirthday = request.Dateofbirthday,
                ShareholderStatus = request.ShareholderStatus,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address1 = request.Address1,
            };
            await hRRepository.UpdateEmployeeAsync(response);
            return Ok(response);
        }
    }
}
