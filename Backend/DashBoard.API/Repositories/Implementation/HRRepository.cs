using DashBoard.API.Data;
using DashBoard.API.Models.Domain;

//using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace DashBoard.API.Repositories.Implementation
{
    public class HRRepository : IHRRepository
    {
        private readonly MysqlContext mysqlContext;
        private readonly SqlServerContext sqlServerContext;

        public HRRepository(MysqlContext mysqlContext, SqlServerContext sqlServerContext)
        {
            this.mysqlContext = mysqlContext;
            this.sqlServerContext = sqlServerContext;
        }

        public async Task<HRUpdateEmployeeDto?> FindEmployee(decimal employmentId)
        {
            var employeeMysql = await mysqlContext.Employees.FirstOrDefaultAsync(x => x.IdEmployee == employmentId);
            var employeeSql = await sqlServerContext.Employments.Include(x => x.Personal).FirstOrDefaultAsync(x => x.EmploymentId == employmentId);
            if (employeeMysql is not null && employeeSql is not null)
            {
                return new HRUpdateEmployeeDto
                {
                    EmploymentId = employeeMysql.EmployeeNumber,
                    LastName = employeeMysql.LastName,
                    FirstName = employeeMysql.FirstName,
                    PhoneNumber = employeeSql.Personal?.CurrentPhoneNumber,
                    ShareholderStatus = employeeSql.Personal?.ShareholderStatus,
                    Email = employeeSql.Personal?.CurrentPersonalEmail,
                    Address = employeeSql.Personal?.CurrentAddress1
                };
            }
            return null;
        }

        public async Task<IEnumerable<BenefitPlan>> FindAllBenefitPlan()
        {
            return await sqlServerContext.BenefitPlans.ToListAsync();
        }

        public async Task<CreateEmployeeDto> CreateEployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = CreateObjectPayroll(createEmployeeDto);
            var employment = CreateObjectHR(createEmployeeDto);

            await mysqlContext.Employees.AddAsync(employee);

            await mysqlContext.SaveChangesAsync();

            await sqlServerContext.Employments.AddAsync(employment);
            await sqlServerContext.SaveChangesAsync();

            return createEmployeeDto;
        }

        private uint GetAllIdEmployee()
        {
            return mysqlContext.Employees.Max(e => e.EmployeeNumber);
        }

        private decimal GetAllIdJobHistory()
        {
            return sqlServerContext.JobHistory.Max(e => e.JobHistoryId);
        }
        private Employee CreateObjectPayroll(CreateEmployeeDto dto)
        {
            return new Employee
            {
                IdEmployee = dto.IdEmployee,
                EmployeeNumber = GetAllIdEmployee() + 1,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Ssn = dto.Ssn,
                PayRatesIdPayRates = dto.PayRatesIdPayRates,
            };
        }
        private Employment CreateObjectHR(CreateEmployeeDto dto)
        {
            Personal personal = new Personal
            {
                PersonalId = dto.PersonalId,
                CurrentFirstName = dto.FirstName,
                CurrentLastName = dto.LastName,
                SocialSecurityNumber = dto.Ssn.ToString(),
                CurrentAddress1 = dto.Address,
                CurrentCity = dto.City,
                CurrentGender = dto.Gender,
                ShareholderStatus = dto.ShareholderStatus,
                BenefitPlanId = dto.BenefitPlanId,
            };
            JobHistory jobHistory = new JobHistory
            {
                JobHistoryId = GetAllIdJobHistory() + 1,
                Department = dto.Department,
            };
            Employment employment = new Employment
            {
                EmploymentId = dto.IdEmployee,
                Personal = personal,
            };
            employment.JobHistories.Add(jobHistory);
            return employment;
        }

        public async Task<HRUpdateEmployeeDto?> UpdateEmployeeAsync(HRUpdateEmployeeDto updateEmployeeDto)
        {
            if (!await EmployeeExistsInSqlServer(updateEmployeeDto.EmploymentId))
            {
                return null;
            }

            await UpdateEmployeeInMySql(updateEmployeeDto);
            await UpdateEmployeeInSqlServer(updateEmployeeDto);

            return updateEmployeeDto;
        }

        //kiểm tra employee có tồn tại hay không
        private async Task<bool> EmployeeExistsInSqlServer(decimal employeeId)
        {
            return await sqlServerContext.Employments.AnyAsync(x => x.EmploymentId == employeeId);
        }

        private async Task UpdateEmployeeInMySql(HRUpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await mysqlContext.Employees
                //.Include(x => x.Birthday)
                .FirstOrDefaultAsync(x => x.IdEmployee == updateEmployeeDto.EmploymentId);
            if (employee is not null)
            {
                employee.FirstName = updateEmployeeDto.FirstName;
                employee.LastName = updateEmployeeDto.LastName;
                //if (employee.Birthday == null)
                //{
                //    employee.Birthday = new Birthday(); // Giả sử Birthday là tên lớp chính xác
                //}
                //employee.Birthday.Dateofbirthday = updateEmployeeDto.Dateofbirthday;
                //employee.Birthday.Dateofbirthday = updateEmployeeDto.Dateofbirthday;

                await mysqlContext.SaveChangesAsync();
            }
        }

        private async Task UpdateEmployeeInSqlServer(HRUpdateEmployeeDto updateEmployeeDto)
        {
            var existingEmployee = await sqlServerContext.Employments
                .FirstOrDefaultAsync(x => x.EmploymentId == updateEmployeeDto.EmploymentId);

            if (existingEmployee is not null)
            {
                sqlServerContext.Entry(existingEmployee).CurrentValues.SetValues(updateEmployeeDto);
                await sqlServerContext.SaveChangesAsync();
            }
        }
    }
}

