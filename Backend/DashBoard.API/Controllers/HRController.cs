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
        public async Task<IActionResult> GetEmployeeById(decimal id)
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

        [HttpGet]
        public async Task<IActionResult> GetBenefitPlan()
        {
            var benefit = await hRRepository.FindAllBenefitPlan();
            return Ok(benefit);
        }
        [HttpPut]
        [Route("{idEmployee}")]
        public async Task<IActionResult> EditEmployee([FromRoute] int idEmployee, HRUpdateEmployeeDto request)
        {
            //map dto to domain model
            var response = new HRUpdateEmployeeDto
            {
                EmploymentId = idEmployee,
                LastName = request.LastName,
                FirstName = request.FirstName,                
                ShareholderStatus = request.ShareholderStatus,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
            };
            await hRRepository.UpdateEmployeeAsync(response);
            return Ok(response);
        }
    }
}
