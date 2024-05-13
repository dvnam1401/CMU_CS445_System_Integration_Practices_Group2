using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class DeletedRepository : IDeletedRepository
    {
        private readonly MysqlContext mysqlContext;
        private readonly SqlServerContext sqlServerContext;

        public DeletedRepository(MysqlContext mysqlContext, SqlServerContext sqlServerContext)
        {
            this.mysqlContext = mysqlContext;
            this.sqlServerContext = sqlServerContext;
        }

        public async Task DeleteEmployeeAsync(int employeeCode)
        {
            // Bước 1: Tìm nhân viên thông qua EmployeeCode
            var employees = await mysqlContext.Employees
                                             .Where(e => e.EmployeeNumber == employeeCode)
                                             .ToListAsync();
            if (employees.Any())
            {
                foreach (var employee in employees)
                {
                    var employmentWorkingTimes = await sqlServerContext.EmergencyContacts
                        .Where(wt => wt.EmploymentId == employee.IdEmployee)
                        .ToListAsync();
                    var employmentJobHistory = await sqlServerContext.JobHistory
                        .Where(wt => wt.EmploymentId == employee.IdEmployee)
                        .ToListAsync();
                    sqlServerContext.JobHistory.RemoveRange(employmentJobHistory);
                    sqlServerContext.EmergencyContacts.RemoveRange(employmentWorkingTimes);
                }
            }

            var employments = await sqlServerContext.Employments                                           
                                           .Where(em => em.EmploymentCode == employeeCode.ToString())
                                           .ToListAsync();

            sqlServerContext.Employments.RemoveRange(employments);
            mysqlContext.Employees.RemoveRange(employees);

            // Lưu các thay đổi
            await sqlServerContext.SaveChangesAsync();
            await mysqlContext.SaveChangesAsync();
        }

        public async Task DeletePersonalAsync(decimal personalId)
        {
            // Check if any Employment records are associated with this PersonalId
            var employmentExists = await sqlServerContext.Employments
                .AnyAsync(e => e.PersonalId == personalId);

            if (employmentExists)
            {
                throw new InvalidOperationException("Cannot delete personal record because related employment records exist.");
            }

            // Retrieve the Personal record to delete
            var personalToDelete = await sqlServerContext.Personals
                .FindAsync(personalId);

            if (personalToDelete == null)
            {
                throw new InvalidOperationException("Personal record not found.");
            }

            // Remove the Personal record from the context
            sqlServerContext.Personals.Remove(personalToDelete);

            // Save changes to the database
            await sqlServerContext.SaveChangesAsync();
        }
    }
}
