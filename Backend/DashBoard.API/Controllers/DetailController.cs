using DashBoard.API.Models.Domain;
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
        private readonly ISqlDataRepository sqlDataRepository;
        private readonly IMysqlDataRepository mysqlDataRepository;

        public DetailController(IDetailRepository detailRepository, ISqlDataRepository sqlDataRepository, IMysqlDataRepository mysqlDataRepository)
        {
            this.detailRepository = detailRepository;
            this.sqlDataRepository = sqlDataRepository;
            this.mysqlDataRepository = mysqlDataRepository;
        }

        [HttpGet("getPersonal-by-id")]
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


        [HttpGet("getAll-Employment-byPersonaId")]
        public async Task<IActionResult> GetAllEmploymentByPersonalId(int personalId)
        {
            //try
            //{
                var employeeDto = await detailRepository.GetAllEmployeeByIdAsync(personalId);
                return Ok(employeeDto);
            //}
            //catch (KeyNotFoundException ex)
            //{
            //    return NotFound(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
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

        [HttpGet("getAll-benefit")]
        public async Task<IActionResult> GetAllBenefit()
        {
            try
            {
                var benefit = await sqlDataRepository.GetByAllBenefitPlan();
                List<BenefitPlan> response = new List<BenefitPlan>();
                if (benefit == null)
                {
                    return NotFound();
                }
                foreach (var item in benefit)
                {
                    response.Add(new BenefitPlan
                    {
                        BenefitPlansId = item.BenefitPlansId,
                        PlanName = item.PlanName,
                    });
                }
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("getAll-payRate")]
        public async Task<IActionResult> GetAllPayRate()
        {
            try
            {
                var payRates = await mysqlDataRepository.GetByAllPayRate();
                List<PayRate> response = new List<PayRate>();
                if (payRates == null)
                {
                    return NotFound();
                }
                foreach (var item in payRates)
                {
                    response.Add(new PayRate
                    {
                        IdPayRates = item.IdPayRates,
                        PayRateName = item.PayRateName,
                    });
                }
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

       
    }
}
