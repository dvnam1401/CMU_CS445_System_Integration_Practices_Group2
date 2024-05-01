using DashBoard.API.Models;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

        [HttpGet("getAllDepartement")]
        public async Task<IActionResult> GetDepartment()
        {
            var resule = serviceRepository.GetAllDepartments();
            return Ok(resule);
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

        //[HttpGet("GetAllEmployee")]
        //public async Task<IActionResult> GetAllEmployee()
        //{
        //    var result = await serviceRepository.GetByAllEmployee();
        //    return Ok(result);
        //}

        [HttpPost("filter/employees")]
        //[Route("{filter}")]
        //<IEnumerable<EmployeeSalaryDto>>
        public async Task<ActionResult> GetEmployeeSalary([FromBody] EmployeeFilterDto filter)
        {
            List<ViewTotal> view = new List<ViewTotal>();
            try
            {
                var employees = await serviceRepository.GetEmployeesSalary(filter);
                foreach (var employee in employees)
                {
                    view.Add(new ViewTotal
                    {
                        FullName = employee.FullName,
                        Gender = employee.Gender,
                        Ethnicity = employee.Ethnicity,
                        Category = employee.Category,
                        Department = employee.JobHistories?.FirstOrDefault()?.Department,
                        Total = employee.TotalSalary,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("filter/number-vacation-days")]
        //<IEnumerable<NumberOfVacationDay>>
        public async Task<ActionResult> GetNumberVacationDay([FromBody] EmployeeFilterDto filter)
        {
            List<ViewTotal> view = new List<ViewTotal>();
            try
            {
                var employees = await serviceRepository.GetNumberOfVacationDays(filter);
                foreach (var employee in employees)
                {
                    view.Add(new ViewTotal
                    {
                        FullName = employee.FullName,
                        Gender = employee.Gender,
                        Ethnicity = employee.Ethnicity,
                        Category = employee.Category,
                        Department = employee.JobHistories?.FirstOrDefault()?.Department,
                        Total = employee.TotalDaysOff,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("filter/average-benefit")]
        public async Task<ActionResult> GetAverageBenefit([FromBody] EmployeeFilterDto filter)
        {
            List<ViewTotal> view = new List<ViewTotal>();
            try
            {
                var employees = await serviceRepository.GetEmployeeAverageBenefit(filter);
                foreach (var employee in employees)
                {
                    view.Add(new ViewTotal
                    {
                        FullName = employee.FullName,
                        Gender = employee.Gender,
                        Ethnicity = employee.Ethnicity,
                        Category = employee.Category,
                        Department = employee.JobHistories?.FirstOrDefault()?.Department,
                        Total = employee.TotalAverageBenefit,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetEmployeeAnniversary")]
        public async Task<ActionResult<IEnumerable<EmployeeAnniversaryDto>>> GetEmployeeAnniversary([FromQuery] int daysLimit)
        {
            List<EmployeeAnniversaryDto> view = new List<EmployeeAnniversaryDto>();
            try
            {
                var employments = await serviceRepository.GetEmployeesAnniversaryInfo(daysLimit);
                foreach (var employee in employments)
                {
                    view.Add(new EmployeeAnniversaryDto
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetEmployeeBirthday")]
        public async Task<ActionResult<IEnumerable<EmployeeAnniversaryDto>>> GetEmployeeBirthday([FromQuery] int daysLimit = 300)
        {
            List<EmployeeAnniversaryDto> view = new List<EmployeeAnniversaryDto>();
            try
            {
                var employments = await serviceRepository.GetEmployeesWithBirthdaysThisMonth(daysLimit);
                foreach (var employee in employments)
                {
                    view.Add(new EmployeeAnniversaryDto
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("CountVacationEmployee")]
        public async Task<ActionResult<IEnumerable<EmployeeVacationDto>>> GetVacationEmployeeThisYear([FromQuery] int minimumDays = 12)
        {
            List<EmployeeVacationDto> view = new List<EmployeeVacationDto>();
            try
            {
                var employments = await serviceRepository.GetEmployeesWithAccumulatedVacationDays(minimumDays);
                foreach (var employee in employments)
                {
                    view.Add(new EmployeeVacationDto
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAllVacation")]
        public async Task<ActionResult<IEnumerable<EmployeeVacationDto>>> GetAllVacation()
        {
            List<EmployeeVacationDto> view = new List<EmployeeVacationDto>();
            try
            {
                var employmentAccumulatedVacation = await serviceRepository.GetEmployeesWithAccumulatedVacationDays(12);
                var employmentWithBirthday = await serviceRepository.GetEmployeesWithBirthdaysThisMonth(15);
                var employmentAnniversary = await serviceRepository.GetEmployeesAnniversaryInfo(15);

                foreach (var employee in employmentAccumulatedVacation)
                {
                    view.Add(new EmployeeVacationDto
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                foreach (var employee in employmentWithBirthday)
                {
                    view.Add(new EmployeeVacationDto
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                foreach (var employee in employmentAnniversary)
                {
                    view.Add(new EmployeeVacationDto
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
