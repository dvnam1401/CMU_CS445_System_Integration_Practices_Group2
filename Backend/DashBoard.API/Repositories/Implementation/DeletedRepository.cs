using DashBoard.API.Data;
using DashBoard.API.Models.Domain;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        //public async Task DeleteEmployeeAsync(int personalIdCode)
        //{
        //    var employments = await sqlServerContext.Employments
        //        .Include(e => e.JobHistories)
        //        .Include(e => e.EmploymentWorkingTimes)
        //        .Where(em => em.PersonalId == Convert.ToUInt64(personalIdCode))
        //                                   //.Select(em => em.EmploymentCode)
        //                                   .ToListAsync();

        //    // Chuyển EmploymentCode sang string nếu cần
        //    var employeeNumbers = employments.Select(em => em.EmploymentCode.ToString()).Distinct().ToList();
        //    // Tìm và xóa các Employee
        //    var employees = await mysqlContext.Employees
        //        .Where(e => employeeNumbers.Contains(e.EmployeeNumber.ToString()))
        //        .ToListAsync();

        //    if (employees.Any())
        //    {
        //        foreach (var employee in employees)
        //        {
        //            var employmentWorkingTimes = await sqlServerContext.EmergencyContacts
        //                .Where(wt => wt.EmploymentId == employee.IdEmployee)
        //                .ToListAsync();
        //            var employmentJobHistory = await sqlServerContext.JobHistory
        //                .Where(wt => wt.EmploymentId == employee.IdEmployee)
        //                .ToListAsync();
        //            sqlServerContext.JobHistory.RemoveRange(employmentJobHistory);
        //            sqlServerContext.EmergencyContacts.RemoveRange(employmentWorkingTimes);
        //        }
        //    }

        //    sqlServerContext.Employments.RemoveRange(employments);
        //    mysqlContext.Employees.RemoveRange(employees);

        //    // Lưu các thay đổi
        //    await sqlServerContext.SaveChangesAsync();
        //    await mysqlContext.SaveChangesAsync();
        //}

        public async Task DeleteEmployeeAsync(int personalIdCode)
        {
            var employments = await sqlServerContext.Employments
                                           .Include(em => em.EmploymentWorkingTimes)  // Thêm phần này để load các EmploymentWorkingTime
                                           .Include(em => em.JobHistories)           // Thêm phần này để load các JobHistory
                                           .Where(em => em.PersonalId == Convert.ToUInt64(personalIdCode))
                                           .ToListAsync();

            if (employments.Any())
            {
                // Xóa các bản ghi từ EmploymentWorkingTime và JobHistory trước
                foreach (var employment in employments)
                {
                    sqlServerContext.EmergencyContacts.RemoveRange(employment.EmploymentWorkingTimes);
                    sqlServerContext.JobHistory.RemoveRange(employment.JobHistories);
                }

                // Xóa các bản ghi Employment
                sqlServerContext.Employments.RemoveRange(employments);
                await sqlServerContext.SaveChangesAsync();  // Đảm bảo thay đổi được lưu trước khi xử lý tiếp các bảng khác
            }

            // Tiếp tục xử lý xóa Employee từ MySQL
            var employeeNumbers = employments.Select(em => em.EmploymentCode.ToString()).Distinct().ToList();
            var employees = await mysqlContext.Employees
                .Where(e => employeeNumbers.Contains(e.EmployeeNumber.ToString()))
                .ToListAsync();

            if (employees.Any())
            {
                mysqlContext.Employees.RemoveRange(employees);
                await mysqlContext.SaveChangesAsync();
            }
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
