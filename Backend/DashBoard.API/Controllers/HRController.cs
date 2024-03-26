using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRController : ControllerBase
    {
        private readonly IHRRepository hRRepository;

        public HRController(IHRRepository hRRepository)
        {
            this.hRRepository = hRRepository;
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
