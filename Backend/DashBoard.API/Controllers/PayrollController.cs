using DashBoard.API.Models;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;
using DashBoard.API.Repositories.Inteface;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "PayRoll")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollRepository payrollRepository;

        public PayrollController(IPayrollRepository payrollRepository)
        {
            this.payrollRepository = payrollRepository;
        }
        // GET: api/payroll/{idEmployee
        [HttpGet]
        [Route("{idEmployee}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int idEmployee)
        {
            var existingEmployee = await payrollRepository.GetEmployeeById(idEmployee);
            if (existingEmployee is null)
            {
                return NotFound();
            }
            //convert domain model to dto
            var respone = new EmployeeDto
            {
                EmployeeNumber = existingEmployee.EmployeeNumber,
                LastName = existingEmployee.LastName,
                FirstName = existingEmployee.FirstName,
                Ssn = existingEmployee.Ssn,
                PayRate = existingEmployee.PayRate,
                PayRatesIdPayRates = existingEmployee.PayRatesIdPayRates,
                VacationDays = existingEmployee.VacationDays,
                PaidToDate = existingEmployee.PaidToDate,
                PaidLastYear = existingEmployee.PaidLastYear,
                PayRates = new PayRate
                {
                    IdPayRates = existingEmployee.PayRates.IdPayRates,
                    PayRateName = existingEmployee.PayRates.PayRateName,
                    Value = existingEmployee.PayRates.Value,
                    TaxPercentage = existingEmployee.PayRates.TaxPercentage,
                    PayType = existingEmployee.PayRates.PayType,
                    PayAmount = existingEmployee.PayRates.PayAmount,
                    PtLevelC = existingEmployee.PayRates.PtLevelC,
                },
            };
            return Ok(respone);
        }

        //GET: api/payroll
        [HttpGet]
        public async Task<IActionResult> GetAllPayRate()
        {
            var payRates = await payrollRepository.GetAllPayRates();

            //convert domain model to dto
            var response = new List<PayRateDto>();
            foreach (var payRate in payRates)
            {
                response.Add(new PayRateDto
                {
                    PayRateName = payRate.PayRateName,
                    Value = payRate.Value,
                    TaxPercentage = payRate.TaxPercentage,
                    PayType = payRate.PayType,
                    PayAmount = payRate.PayAmount,
                    PtLevelC = payRate.PtLevelC
                });
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("{idEmployee}")]
        public async Task<IActionResult> EditEmployee([FromRoute] int idEmployee, PayRollUpdateEmployeeDto request)
        {
            //map dto to domain model
            var employee = new PayRollUpdateEmployeeDto
            {
                EmployeeNumber = (uint)idEmployee,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Ssn = request.Ssn,
                PayRatesIdPayRates = request.PayRatesIdPayRates,
            };
            await payrollRepository.UpdateEmployeeAsync(employee);
            return Ok(employee);
        }
    }

}

