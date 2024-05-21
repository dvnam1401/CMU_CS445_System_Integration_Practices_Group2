using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using System.Threading.Tasks;

namespace DashBoard.API.Repositories.Implementation
{
    public class CreatedRepository : ICreatedRepository
    {
        private readonly MysqlContext mysqlContext;
        private readonly SqlServerContext sqlServerContext;

        public CreatedRepository(MysqlContext mysqlContext, SqlServerContext sqlServerContext)
        {
            this.mysqlContext = mysqlContext;
            this.sqlServerContext = sqlServerContext;
        }
        // tạo hồ sơ nhân sự
        public async Task CreatePersonalAsync(CreatePersonalDto createPersonal)
        {
            var tempPer = new Personal
            {
                PersonalId = createPersonal.PersonalId,
                CurrentFirstName = createPersonal.CurrentFirstName,
                CurrentLastName = createPersonal.CurrentLastName,
                CurrentMiddleName = createPersonal.CurrentMiddleName,
                BirthDate = createPersonal.BirthDate,
                SocialSecurityNumber = createPersonal.SocialSecurityNumber,
                DriversLicense = createPersonal.DriversLicense,
                CurrentAddress1 = createPersonal.CurrentAddress1,
                CurrentAddress2 = createPersonal.CurrentAddress2,
                CurrentCity = createPersonal.CurrentCity,
                CurrentCountry = createPersonal.CurrentCountry,
                CurrentZip = createPersonal.CurrentZip,
                CurrentGender = createPersonal.CurrentGender,
                CurrentPhoneNumber = createPersonal.CurrentPhoneNumber,
                CurrentPersonalEmail = createPersonal.CurrentPersonalEmail,
                CurrentMaritalStatus = createPersonal.CurrentMaritalStatus,
                Ethnicity = createPersonal.Ethnicity,
                ShareholderStatus = createPersonal.ShareholderStatus,
                BenefitPlanId = createPersonal.BenefitPlanId,
            };
            await sqlServerContext.Personals.AddAsync(tempPer);
            await sqlServerContext.SaveChangesAsync();
        }

        private int GetAllIdEmployee()
        {
            var result = mysqlContext.Employees
                         .Select(e => (int?)e.IdEmployee)                         
                         .Max() ?? 0; // If Max returns null, replace with 0

            return result;
        }

        private int GetAllIdJobHistory()
        {
            var result = sqlServerContext.JobHistory
                         .Select(e => (int?)e.JobHistoryId)                         
                         .Max() ?? 0; // If Max returns null, replace with 0
            return result;
        }

        private decimal GetAllIdWorkingTime()
        {
            var result = sqlServerContext.EmergencyContacts
                         .Select(e => (decimal?)e.EmploymentWorkingTimeId)
                         .Max()?? 0;
            return result;
        }

        // taọ nhân viên mới
        public async Task CreateEmployeeAsync(CreateEmployeeDto employment)
        {
            var checkEmployee = await mysqlContext.Employees.FindAsync(Convert.ToUInt32(employment.EmploymentCode));
            if (checkEmployee is null)
            {
                var tempEmployee = AddEmployee(employment);
                await mysqlContext.Employees.AddAsync(tempEmployee);
            }

            var tempEmployment = AddEmployment(employment);
            var tempWorkingTime = AddEmploymentWorkingTime(employment);
            var tempJobHistory = AddJobHistory(employment);
            await sqlServerContext.Employments.AddAsync(tempEmployment);
            await sqlServerContext.JobHistory.AddAsync(tempJobHistory);
            await sqlServerContext.EmergencyContacts.AddAsync(tempWorkingTime);
            await mysqlContext.SaveChangesAsync();
            await sqlServerContext.SaveChangesAsync();
        }

        private JobHistory AddJobHistory(CreateEmployeeDto employment)
        {
            return new JobHistory
            {
                JobHistoryId = GetAllIdJobHistory() + 1,
                EmploymentId = GetAllIdEmployee() + 1,
                Department = employment.Department,
                FromDate = employment.HireDateForWorking,
            };
        }

        private Employee AddEmployee(CreateEmployeeDto emp)
        {
            return new Employee
            {
                IdEmployee = GetAllIdEmployee() + 1,
                EmployeeNumber = UInt32.Parse(emp.EmploymentCode),
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Ssn = emp.Ssn,
                PayRate = emp.PayRate,
                PayRatesIdPayRates = emp.PayRatesIdPayRates,
            };
        }

        private Employment AddEmployment(CreateEmployeeDto emp)
        {
            return new Employment
            {
                EmploymentId = GetAllIdEmployee() + 1,
                EmploymentCode = emp.EmploymentCode,
                EmploymentStatus = emp.EmploymentStatus,
                HireDateForWorking = emp.HireDateForWorking,
                NumberDaysRequirementOfWorkingPerMonth = emp.NumberDaysRequirementOfWorkingPerMonth,
                PersonalId = emp.PersonalId,
            };
        }

        private EmploymentWorkingTime AddEmploymentWorkingTime(CreateEmployeeDto emp)
        {
            return new EmploymentWorkingTime
            {
                EmploymentWorkingTimeId = GetAllIdWorkingTime() + 1,
                EmploymentId = GetAllIdEmployee() + 1,
                YearWorking = emp.HireDateForWorking,
            };
        }
    }
}
