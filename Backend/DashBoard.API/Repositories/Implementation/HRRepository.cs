using DashBoard.API.Data;
using DashBoard.API.Models;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Employee> FindEmployee(int id)
        {
            return await mysqlContext.Employees.FirstOrDefaultAsync(x => x.EmployeeNumber == id);
        }

        public async Task<CreateEmployeeDto> CreateEployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = CreateEmployeeObject(createEmployeeDto);
            var birthday = CreateBirthdayObject(createEmployeeDto);
            var personal = CreatePersonalObject(createEmployeeDto);

            await mysqlContext.Employees.AddAsync(employee);
            await mysqlContext.Birthdays.AddAsync(birthday);
            await mysqlContext.SaveChangesAsync();

            await sqlServerContext.Personals.AddAsync(personal);
            await sqlServerContext.SaveChangesAsync();

            return createEmployeeDto;
        }

        private Employee CreateEmployeeObject(CreateEmployeeDto dto)
        {
            return new Employee
            {
                IdEmployee = dto.IdEmployee,
                EmployeeNumber = dto.EmployeeNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Ssn = dto.Ssn,
                PayRatesIdPayRates = dto.PayRatesIdPayRates,
            };
        }

        private Birthday CreateBirthdayObject(CreateEmployeeDto dto)
        {
            return new Birthday
            {
                Dateofbirthday = dto.Dateofbirthday,
                EmployeeNumber = dto.EmployeeNumber,
            };
        }

        private Personal CreatePersonalObject(CreateEmployeeDto dto)
        {
            return new Personal
            {
                EmployeeId = dto.EmployeeNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                ShareholderStatus = dto.ShareholderStatus,
                Email = dto.Email,
                Address1 = dto.Address1
            };
        }    

        public async Task<HRUpdateEmployeeDto?> UpdateEmployeeAsync(HRUpdateEmployeeDto updateEmployeeDto)
        {
            if (!await EmployeeExistsInSqlServer(updateEmployeeDto.EmployeeId))
            {
                return null;
            }

            await UpdateEmployeeInMySql(updateEmployeeDto);
            await UpdateEmployeeInSqlServer(updateEmployeeDto);

            return updateEmployeeDto;
        }

        private async Task<bool> EmployeeExistsInSqlServer(decimal employeeId)
        {
            return await sqlServerContext.Personals.AnyAsync(x => x.EmployeeId == employeeId);
        }

        private async Task UpdateEmployeeInMySql(HRUpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await mysqlContext.Employees
                .Include(x => x.Birthday)
                .FirstOrDefaultAsync(x => x.EmployeeNumber == updateEmployeeDto.EmployeeId);

            if (employee != null)
            {
                employee.FirstName = updateEmployeeDto.FirstName;
                employee.LastName = updateEmployeeDto.LastName;
                employee.Birthday.Dateofbirthday = updateEmployeeDto.Dateofbirthday;

                await mysqlContext.SaveChangesAsync();
            }
        }

        private async Task UpdateEmployeeInSqlServer(HRUpdateEmployeeDto updateEmployeeDto)
        {
            var existingEmployee = await sqlServerContext.Personals
                .FirstOrDefaultAsync(x => x.EmployeeId == updateEmployeeDto.EmployeeId);

            if (existingEmployee != null)
            {
                sqlServerContext.Entry(existingEmployee).CurrentValues.SetValues(updateEmployeeDto);
                await sqlServerContext.SaveChangesAsync();
            }
        }

    }
}

