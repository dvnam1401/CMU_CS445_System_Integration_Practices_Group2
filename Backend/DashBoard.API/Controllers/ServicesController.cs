using DashBoard.API.Models;
using DashBoard.API.Models.DTO;
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
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] uint id)
        {
            var existing = await serviceRepository.GetBenefitPlanById(id);
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

        [HttpPost("filter/employees")]
        //[Route("{filter}")]
        public async Task<ActionResult<IEnumerable<EmployeeSalaryDto>>> GetEmployeeSalary([FromBody] EmployeeFilterDto filter)
        {
            try
            {
                var employees = await serviceRepository.GetEmployeesSalary(filter);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("filter/number-vacation-days")]
        public async Task<ActionResult<IEnumerable<NumberOfVacationDay>>> GetNumberVacationDay([FromBody] EmployeeFilterDto filter)
        {
            try
            {
                var employees = await serviceRepository.GetNumberOfVacationDays(filter);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetEmployeeAnniversary")]
        //[Route("{daysLimit}")]

        public async Task<ActionResult<IEnumerable<EmployeeAnniversaryDto>>> GetEmployeeAnniversary([FromQuery] int daysLimit)
        {
            var result = await serviceRepository.GetEmployeesAnniversaryInfo(daysLimit);
            return Ok(result);
        }
    }
}
