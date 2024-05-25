using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummarizedController : ControllerBase
    {
        private readonly ISummarizedRepository summarizedRepository;
        public SummarizedController(ISummarizedRepository summarizedRepository)
        {
            this.summarizedRepository = summarizedRepository;
        }

        [HttpGet("getAllDepartement")]
        public async Task<IActionResult> GetDepartment()
        {
            var resule = summarizedRepository.GetAllDepartments();
            return Ok(resule);
        }

        [HttpPost("filter/employees")]
        //[Route("{filter}")]
        //<IEnumerable<EmployeeSalaryDto>>
        public async Task<ActionResult> GetEmployeeSalary([FromBody] EmployeeFilterDto filter)
        {
            List<ViewTotal> view = new List<ViewTotal>();
            try
            {
                var employees = await summarizedRepository.GetEmployeesSalary(filter);
                foreach (var employee in employees)
                {
                    view.Add(new ViewTotal
                    {
                        FullName = employee.FullName,
                        Gender = employee.Gender.ToUpper(),
                        Ethnicity = employee.Ethnicity,
                        Category = employee.Category,
                        ShareholderStatus = employee.ShareholderStatus,
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
            //try
            //{
                var employees = await summarizedRepository.GetNumberOfVacationDays(filter);
                foreach (var employee in employees)
                {
                    view.Add(new ViewTotal
                    {
                        FullName = employee.FullName,
                        Gender = employee.Gender.ToUpper(),
                        Ethnicity = employee.Ethnicity,
                        Category = employee.Category,
                        ShareholderStatus = employee.ShareholderStatus,
                        Department = employee.JobHistories?.FirstOrDefault()?.Department,
                        Total = employee.TotalDaysOff,
                    });
                }
                return Ok(view);
           // }
            //catch (Exception ex)
            //{
            //    // Xử lý lỗi
            //    return StatusCode(500, ex.Message);
            //}
        }

        [HttpPost("filter/average-benefit")]
        public async Task<ActionResult> GetAverageBenefit([FromBody] EmployeeFilterDto filter)
        {
            List<ViewTotal> view = new List<ViewTotal>();
            try
            {
                var employees = await summarizedRepository.GetEmployeeAverageBenefit(filter);
                foreach (var employee in employees)
                {
                    view.Add(new ViewTotal
                    {
                        FullName = employee.FullName,
                        Gender = employee.Gender,
                        Ethnicity = employee.Ethnicity,
                        ShareholderStatus = employee.ShareholderStatus,
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
    }
}
