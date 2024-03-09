using DashBoard.API.Data;
using DashBoard.API.Models;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly SqlServerContext? dbContext;
        private readonly MysqlContext? dbMysqlContext;

        public ServiceRepository(SqlServerContext? dbContext, MysqlContext? dbMysqlContext)
        {
            this.dbContext = dbContext;
            this.dbMysqlContext = dbMysqlContext;
        }

        public async Task<IEnumerable<BenefitPlan?>> GetByAll()
        {
            return await dbContext.BenefitPlans.ToListAsync();
        }

        public async Task<BenefitPlan?> GetById(int benefit)
        {
            return await dbContext.BenefitPlans.FirstOrDefaultAsync(x => x.BenefitPlanId == benefit);
        }

        public async Task<IEnumerable<Employee?>> GetByAllEmployee()
        {
            return await dbMysqlContext.Employees.ToListAsync();
        }
    }
}
