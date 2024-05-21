using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class EditRepository : IEditRepository
    {
        private readonly MysqlContext mysqlContext;
        private readonly SqlServerContext sqlServerContext;

        public EditRepository(MysqlContext mysqlContext, SqlServerContext sqlServerContext)
        {
            this.mysqlContext = mysqlContext;
            this.sqlServerContext = sqlServerContext;
        }
        //public async Task EditEmployeeAsync(EditEmployeeDto employeeDto)
        //{
        //    // Lấy thông tin nhân viên hiện tại
        //    var tempEmp = await mysqlContext.Employees
        //                          .FirstOrDefaultAsync(e => e.IdEmployee == employeeDto.IdEmployee);
        //    var employees = await mysqlContext.Employees
        //                            .Where(e => e.EmployeeNumber == tempEmp.EmployeeNumber)
        //                            .ToListAsync();
        //    if (!employees.Any())
        //    {
        //        throw new Exception("Employee not found");
        //    }
        //    bool isUpdated = false;
        //    foreach (var employee in employees)
        //    {
        //        int count = 0;
        //        if (employee.FirstName != employeeDto.FirstName)
        //        {
        //            employee.FirstName = employeeDto.FirstName;
        //            isUpdated = true;
        //            count++;
        //        }
        //        if (employee.LastName != employeeDto.LastName)
        //        {
        //            employee.LastName = employeeDto.LastName;
        //            isUpdated = true;
        //            count++;
        //        }
        //        if (employee.Ssn != employeeDto.Ssn)
        //        {
        //            employee.Ssn = employeeDto.Ssn;
        //            isUpdated = true;
        //            count++;
        //        }
        //        if (count == 0)
        //        {
        //            break;
        //        }
        //    }
        //    //tempEmp.PayRate = await mysqlContext.Payrates
        //    //                        .Where(pr => pr.IdPayRates == employeeDto.PayRatesIdPayRates)
        //    //                        .Select(pr => pr.PayRateName)
        //    //                        .FirstOrDefaultAsync();
        //    tempEmp.PayRate = employeeDto.PayRate;
        //    tempEmp.PayRatesIdPayRates = employeeDto.PayRatesIdPayRates;

        //    var editEmployment = await sqlServerContext.Employments
        //        .Include(e => e.Personal)
        //        .FirstOrDefaultAsync(e => e.EmploymentId == Convert.ToDecimal(employeeDto.IdEmployee));
        //    if (isUpdated)
        //    {
        //        editEmployment.Personal.CurrentFirstName = employeeDto.FirstName;
        //        editEmployment.Personal.CurrentLastName = employeeDto.LastName;
        //        editEmployment.Personal.CurrentMiddleName = "";
        //        editEmployment.Personal.SocialSecurityNumber = employeeDto.Ssn.ToString();
        //    }
        //    editEmployment.EmploymentStatus = employeeDto.EmploymentStatus;
        //    editEmployment.EmploymentCode = employeeDto.EmploymentCode.ToString();
        //    editEmployment.HireDateForWorking = employeeDto.HireDateForWorking;
        //    editEmployment.NumberDaysRequirementOfWorkingPerMonth = employeeDto.NumberDaysRequirementOfWorkingPerMonth;
        //    var editEmploymentWorkingTime = await sqlServerContext.EmergencyContacts.FirstOrDefaultAsync(e => e.EmploymentId == Convert.ToDecimal(employeeDto.IdEmployee));
        //    if (editEmploymentWorkingTime.YearWorking != employeeDto.HireDateForWorking)
        //    {
        //        editEmploymentWorkingTime.YearWorking = employeeDto.HireDateForWorking;
        //    }

        //    var editJobHistory = await sqlServerContext.JobHistory.FirstOrDefaultAsync(e => e.EmploymentId == Convert.ToDecimal(employeeDto.IdEmployee));
        //    if (editJobHistory.Department != employeeDto.Department)
        //    {
        //        editJobHistory.Department = employeeDto.Department;
        //    }
        //    await mysqlContext.SaveChangesAsync();
        //    await sqlServerContext.SaveChangesAsync();
        //}

        public async Task EditEmployeeAsync(EditEmployeeDto employeeDto)
        {
            // Retrieve the specific employee to update PayRate
            var tempEmp = await mysqlContext.Employees
                                 .FirstOrDefaultAsync(e => e.IdEmployee == employeeDto.IdEmployee);
            if (tempEmp == null)
            {
                throw new Exception("Specific employee not found");
            }

            // Get all employees with the same EmployeeNumber for bulk update
            var employees = await GetEmployeesByNumber(tempEmp.EmployeeNumber);
            if (employees == null || !employees.Any())
            {
                throw new Exception("Employee not found");
            }

            // Update information for all fetched employees
            //UpdateEmployeeInfo(employees, employeeDto);

            // Update specific employee's pay information
            UpdateSpecificEmployeePayInfo(tempEmp, employeeDto);

            // Update related employment details
            await UpdateEmploymentDetails(employeeDto);

            await mysqlContext.SaveChangesAsync();
            await sqlServerContext.SaveChangesAsync();
        }

        private async Task<List<Employee>> GetEmployeesByNumber(uint employeeNumber)
        {
            return await mysqlContext.Employees
                                     .Where(e => e.EmployeeNumber == employeeNumber)
                                     .ToListAsync();
        }

        private void UpdateEmployeeInfo(List<Employee> employees, EditEmployeeDto employeeDto)
        {
            foreach (var employee in employees)
            {
                if (employee.FirstName != employeeDto.FirstName)
                {
                    employee.FirstName = employeeDto.FirstName;
                }
                if (employee.LastName != employeeDto.LastName)
                {
                    employee.LastName = employeeDto.LastName;
                }
                if (employee.Ssn != employeeDto.Ssn)
                {
                    employee.Ssn = employeeDto.Ssn;
                }
            }
        }

        private void UpdateSpecificEmployeePayInfo(Employee employee, EditEmployeeDto employeeDto)
        {
            employee.PayRate = employeeDto.PayRate;
            employee.PayRatesIdPayRates = employeeDto.PayRatesIdPayRates;
        }

        private async Task UpdateEmploymentDetails(EditEmployeeDto employeeDto)
        {
            var editEmployment = await sqlServerContext.Employments
                .Include(e => e.Personal)
                .FirstOrDefaultAsync(e => e.EmploymentId == Convert.ToDecimal(employeeDto.IdEmployee));

            if (editEmployment == null) return;

            if (editEmployment.Personal != null)
            {
                editEmployment.Personal.CurrentFirstName = employeeDto.FirstName;
                editEmployment.Personal.CurrentLastName = employeeDto.LastName;
                editEmployment.Personal.CurrentMiddleName = "";
                editEmployment.Personal.SocialSecurityNumber = employeeDto.Ssn.ToString();
            }

            editEmployment.EmploymentStatus = employeeDto.EmploymentStatus;
            editEmployment.HireDateForWorking = employeeDto.HireDateForWorking;
            editEmployment.NumberDaysRequirementOfWorkingPerMonth = employeeDto.NumberDaysRequirementOfWorkingPerMonth;
            var temp = Convert.ToDecimal(employeeDto.IdEmployee);
            var editEmploymentWorkingTime = await sqlServerContext.EmergencyContacts.FirstOrDefaultAsync(e => e.EmploymentId == temp);
            if (editEmploymentWorkingTime != null && editEmploymentWorkingTime.YearWorking != employeeDto.HireDateForWorking)
            {
                editEmploymentWorkingTime.YearWorking = employeeDto.HireDateForWorking;
            }
            else
            {
                var tempWorkingTime =  new EmploymentWorkingTime
                {
                    EmploymentWorkingTimeId = GetAllIdWorkingTime() + 1,
                    EmploymentId = temp,
                    YearWorking = employeeDto.HireDateForWorking,
                };
                await sqlServerContext.EmergencyContacts.AddAsync(tempWorkingTime);

            }

            var editJobHistory = await sqlServerContext.JobHistory.FirstOrDefaultAsync(e => e.EmploymentId == temp);
            if (editJobHistory != null && editJobHistory.Department != employeeDto.Department)
            {
                editJobHistory.Department = employeeDto.Department;
            }
            else
            {
                var tempJobHistory = new JobHistory
                {
                    JobHistoryId = GetAllIdJobHistory() + 1,
                    EmploymentId = temp,
                    Department = employeeDto.Department,
                    FromDate = employeeDto.HireDateForWorking,
                };
                await sqlServerContext.JobHistory.AddAsync(tempJobHistory);
            }

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
                         .Max() ?? 0;
            return result;
        }

        public async Task EditPersonalAsync(UpdatePersonalDto personal)
        {
            var tempPersonal = await sqlServerContext.Personals
                                 .FirstOrDefaultAsync(e => e.PersonalId == personal.PersonalId);
            if (tempPersonal == null)
            {
                throw new Exception("Specific employee not found");
            }

            // Update properties
            tempPersonal.CurrentFirstName = personal.CurrentFirstName;
            tempPersonal.CurrentLastName = personal.CurrentLastName;
            tempPersonal.CurrentMiddleName = personal.CurrentMiddleName;
            tempPersonal.BirthDate = personal.BirthDate; // Make sure BirthDate in your entity is compatible with DateOnly
            tempPersonal.CurrentCity = personal.CurrentCity;
            tempPersonal.SocialSecurityNumber = personal.SocialSecurityNumber;
            tempPersonal.CurrentAddress1 = personal.CurrentAddress1;
            tempPersonal.CurrentZip = personal.CurrentZip;
            tempPersonal.CurrentMaritalStatus = personal.CurrentMaritalStatus;
            tempPersonal.ShareholderStatus = personal.ShareholderStatus;
            tempPersonal.BenefitPlanId = personal.BenefitPlanId;
            tempPersonal.CurrentCountry = personal.CurrentCountry;
            tempPersonal.CurrentAddress2 = personal.CurrentAddress2;
            tempPersonal.CurrentGender = personal.CurrentGender;
            tempPersonal.CurrentPhoneNumber = personal.CurrentPhoneNumber;
            tempPersonal.CurrentPersonalEmail = personal.CurrentPersonalEmail;
            tempPersonal.Ethnicity = personal.Ethnicity;
            tempPersonal.DriversLicense = personal.DriversLicense;

            // Save changes in the database
            await sqlServerContext.SaveChangesAsync();
        }
    }
}
