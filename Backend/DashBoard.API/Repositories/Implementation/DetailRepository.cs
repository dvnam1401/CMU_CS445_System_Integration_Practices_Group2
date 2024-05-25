using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                    BenefitPlanName = p.BenefitPlan.PlanName,
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
            var employment = await sqlServerContext.Employments
                .Include(e => e.Personal)
                .Where(e => e.EmploymentId == id)
                .Select(e => new
                {
                    FirstName = e.Personal.CurrentFirstName,
                    LasName = e.Personal.CurrentLastName,
                    Middle = e.Personal.CurrentMiddleName,
                    SSN = e.Personal.SocialSecurityNumber,
                    EmploymentStatus = e.EmploymentStatus,
                    HireDateForWorking = e.HireDateForWorking,
                    NumberDaysRequirementOfWorkingPerMonth = e.NumberDaysRequirementOfWorkingPerMonth,
                    Department = e.JobHistories.Where(j => j.EmploymentId == e.EmploymentId).Select(j => j.Department).FirstOrDefault(),
                }).FirstOrDefaultAsync();
            // Get employee information from the first database
            var employee = await mysqlContext.Employees
                .Where(e => e.IdEmployee == id)
                .Select(e => new
                {
                    IdEmployee = e.IdEmployee,
                    EmploymentCode = e.EmployeeNumber,
                    PayRate = e.PayRate,
                    PayRatesIdPayRates = e.PayRatesIdPayRates
                })
                .FirstOrDefaultAsync();
            var result = new EmployeeDetailsDto
            {
                IdEmployee = employee.IdEmployee,
                EmploymentCode = employee.EmploymentCode,
                FirstName = employment.FirstName,
                LastName = employment.LasName,
                MiddleName = employment.Middle,
                Ssn = employment.SSN,
                Department = employment.Department,
                PayRate = employee.PayRate,
                PayRatesIdPayRates = employee.PayRatesIdPayRates,
                EmploymentStatus = employment.EmploymentStatus,
                HireDateForWorking = employment.HireDateForWorking,
                NumberDaysRequirementOfWorkingPerMonth = employment.NumberDaysRequirementOfWorkingPerMonth,
            };
            if (employee == null)
            {
                throw new KeyNotFoundException($"An employee with the ID {id} was not found.");
            }

            //// Get employment information from the second database
            //var employment = await sqlServerContext.Employments
            //    .Include(e => e.JobHistories)
            //    .Where(e => e.EmploymentId == Convert.ToDecimal(id))
            //    .Select(e => new
            //    {
            //        EmploymentStatus = e.EmploymentStatus,
            //        HireDateForWorking = e.HireDateForWorking,
            //        NumberDaysRequirementOfWorkingPerMonth = e.NumberDaysRequirementOfWorkingPerMonth,
            //        Department = e.JobHistories.Where(jh => e.EmploymentId == jh.EmploymentId)
            //                    .Select(jh => jh.Department).FirstOrDefault(),
            //    })
            //    .FirstOrDefaultAsync();

            //if (employment != null)
            //{
            //    employee.EmploymentStatus = employment.EmploymentStatus;
            //    employee.HireDateForWorking = employment.HireDateForWorking;
            //    employee.NumberDaysRequirementOfWorkingPerMonth = employment.NumberDaysRequirementOfWorkingPerMonth;
            //    employee.Department = employment.Department;
            //}

            return result;
        }

        public async Task<IEnumerable<EmployeeDetailsDto>> GetAllEmployeeByIdAsync(int personalId)
        {
            // Create a list to temporarily store employee details
            var employeeDetailsList = new List<EmployeeDetailsDto>();

            // Get all employments associated with the given PersonalId
            var employments = await sqlServerContext.Employments
                .Where(e => e.PersonalId == personalId)
                .Include(e => e.Personal)
                .Include(e => e.JobHistories)
                .ToListAsync();
            // Iterate through each employment record to fetch associated employee details
            foreach (var employment in employments)
            {
                var employee = await mysqlContext.Employees
                    .Where(e => e.IdEmployee == employment.EmploymentId)
                    .Select(e => new
                    {
                        e.IdEmployee,
                        e.EmployeeNumber,
                        e.FirstName,
                        e.LastName,
                        e.Ssn,
                        e.PayRate,
                        e.PayRatesIdPayRates
                    })
                    .FirstOrDefaultAsync();

                if (employee != null)
                {
                    var department = employment.JobHistories.FirstOrDefault()?.Department ?? "Unknown";
                    var details = new EmployeeDetailsDto
                    {
                        IdEmployee = employee.IdEmployee,
                        EmploymentCode = employee.EmployeeNumber,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Ssn = employment.Personal.SocialSecurityNumber,
                        PayRate = employee.PayRate,
                        PayRatesIdPayRates = employee.PayRatesIdPayRates,
                        EmploymentStatus = employment.EmploymentStatus,
                        HireDateForWorking = employment.HireDateForWorking,
                        NumberDaysRequirementOfWorkingPerMonth = employment.NumberDaysRequirementOfWorkingPerMonth,
                        Department = department
                    };
                    employeeDetailsList.Add(details);
                }
            }

            // Return the IEnumerable by converting the list to an IEnumerable
            return employeeDetailsList.AsEnumerable();
        }

        public async Task<IEnumerable<EmployeeDetailsDto>> GetAllEmployeeAsync()
        {
            // Get employee information from the first database and order by EmployeeNumber
            var employees = await mysqlContext.Employees
                .Select(e => new EmployeeDetailsDto
                {
                    IdEmployee = e.IdEmployee,
                    EmploymentCode = e.EmployeeNumber, // Assuming EmployeeNumber is the equivalent to EmploymentCode
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PayRate = e.PayRate,
                    PayRatesIdPayRates = e.PayRatesIdPayRates
                })
                .OrderBy(e => e.EmploymentCode)
                .ToListAsync();

            // For each employee, get employment information from the second database
            var employeeIds = employees.Select(e => Convert.ToDecimal(e.IdEmployee)).ToList();
            var employments = await sqlServerContext.Employments
                .Include(e => e.JobHistories)
                .Where(e => employeeIds.Contains(e.EmploymentId))
                .ToListAsync();

            foreach (var employee in employees)
            {
                var employment = employments.FirstOrDefault(e => e.EmploymentId == Convert.ToDecimal(employee.IdEmployee));
                if (employment != null)
                {
                    employee.EmploymentStatus = employment.EmploymentStatus;
                    employee.Ssn = employment.Personal.SocialSecurityNumber;
                    employee.HireDateForWorking = employment.HireDateForWorking;
                    employee.Department = employment.JobHistories.FirstOrDefault()?.Department;
                }
            }

            return employees;
        }
    }
}
