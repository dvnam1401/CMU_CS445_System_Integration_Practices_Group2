using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class DetailRepository : IDetailRepository
    {
        private readonly MysqlContext mysqlContext;
        private readonly SqlServerContext sqlServerContext;

        public DetailRepository(MysqlContext mysqlContext, SqlServerContext sqlServerContext)
        {
            this.mysqlContext = mysqlContext;
            this.sqlServerContext = sqlServerContext;
        }
        public async Task<PersonalDetailDto> GetPersonalByIdAsync(decimal personalId)
        {
            var personal = await sqlServerContext.Personals
                .Where(p => p.PersonalId == personalId)
                .Include(p => p.BenefitPlan)
                .Select(p => new PersonalDetailDto
                {
                    PersonalId = p.PersonalId,
                    CurrentFirstName = p.CurrentFirstName,
                    CurrentLastName = p.CurrentLastName,
                    CurrentMiddleName = p.CurrentMiddleName,
                    BirthDate = p.BirthDate,
                    CurrentGender = p.CurrentGender,
                    CurrentPersonalEmail = p.CurrentPersonalEmail,
                    CurrentPhoneNumber = p.CurrentPhoneNumber,
                    DriversLicense = p.DriversLicense,
                    SocialSecurityNumber = p.SocialSecurityNumber,
                    CurrentAddress1 = p.CurrentAddress1,
                    CurrentZip = p.CurrentZip,
                    CurrentMaritalStatus = p.CurrentMaritalStatus,
                    ShareholderStatus = p.ShareholderStatus,
                    BenefitPlanName= p.BenefitPlan.PlanName,
                    CurrentCountry = p.CurrentCountry,
                    CurrentAddress2 = p.CurrentAddress2,
                    CurrentCity = p.CurrentCity,
                    Ethnicity = p.Ethnicity,
                })
                .FirstOrDefaultAsync();

            if (personal == null)
            {
                throw new InvalidOperationException("Personal record not found.");
            }

            return personal;
        }

        public async Task<IEnumerable<PersonalDetailDto>> GetAllPersonalAsync()
        {
            return await sqlServerContext.Personals
                .Select(p => new PersonalDetailDto
                {
                    PersonalId = p.PersonalId,
                    CurrentFirstName = p.CurrentFirstName,
                    CurrentLastName = p.CurrentLastName,
                    CurrentMiddleName = p.CurrentMiddleName,
                    BirthDate = p.BirthDate,
                    CurrentGender = p.CurrentGender,
                    CurrentPersonalEmail = p.CurrentPersonalEmail,
                    CurrentPhoneNumber = p.CurrentPhoneNumber,
                    CurrentCity = p.CurrentCity,
                }).ToListAsync();
        }

        public async Task<EmployeeDetailsDto> GetEmployeeByIdAsync(int id)
        {
            // Get employee information from the first database
            var employee = await mysqlContext.Employees
                .Where(e => e.IdEmployee == id)
                .Select(e => new EmployeeDetailsDto
                {
                    Id = e.IdEmployee,
                    EmploymentCode = e.EmployeeNumber,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Ssn = e.Ssn,
                    PayRate = e.PayRate,
                    PayRatesIdPayRates = e.PayRatesIdPayRates
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                throw new KeyNotFoundException($"An employee with the ID {id} was not found.");
            }

            // Get employment information from the second database
            var employment = await sqlServerContext.Employments
                .Include(e => e.JobHistories)
                .Where(e => e.EmploymentId == Convert.ToDecimal(id))
                .Select(e => new
                {
                    EmploymentStatus = e.EmploymentStatus,
                    HireDateForWorking = e.HireDateForWorking,
                    Department = e.JobHistories.Where(jh => e.EmploymentId == jh.EmploymentId)
                                .Select(jh => jh.Department).FirstOrDefault(),
                })
                .FirstOrDefaultAsync();

            if (employment != null)
            {
                employee.EmploymentStatus = employment.EmploymentStatus;
                employee.HireDateForWorking = employment.HireDateForWorking;
                employee.Department = employment.Department;
            }

            return employee;
        }

        public async Task<IEnumerable<EmployeeDetailsDto>> GetAllEmployeeAsync()        
        {
            // Get employee information from the first database and order by EmployeeNumber
            var employees = await mysqlContext.Employees
                .Select(e => new EmployeeDetailsDto
                {
                    Id = e.IdEmployee,
                    EmploymentCode = e.EmployeeNumber, // Assuming EmployeeNumber is the equivalent to EmploymentCode
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Ssn = e.Ssn,
                    PayRate = e.PayRate,
                    PayRatesIdPayRates = e.PayRatesIdPayRates
                })
                .OrderBy(e => e.EmploymentCode)
                .ToListAsync();

            // For each employee, get employment information from the second database
            var employeeIds = employees.Select(e => Convert.ToDecimal(e.Id)).ToList();
            var employments = await sqlServerContext.Employments
                .Include(e => e.JobHistories)
                .Where(e => employeeIds.Contains(e.EmploymentId))
                .ToListAsync();

            foreach (var employee in employees)
            {
                var employment = employments.FirstOrDefault(e => e.EmploymentId == Convert.ToDecimal(employee.Id));
                if (employment != null)
                {
                    employee.EmploymentStatus = employment.EmploymentStatus;
                    employee.HireDateForWorking = employment.HireDateForWorking;
                    employee.Department = employment.JobHistories.FirstOrDefault()?.Department;
                }
            }

            return employees;
        }

    }
}
