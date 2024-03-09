using DashBoard.API.Data;
using DashBoard.API.Models;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly SqlServerContext dbContext;

        public ServiceRepository(SqlServerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BenefitPlan?>> GetByAll()
        {
            return await dbContext.BenefitPlans.ToListAsync();
        }

        public async Task<BenefitPlan?> GetById(int benefit)
        {
            return await dbContext.BenefitPlans.FirstOrDefaultAsync(x => x.BenefitPlanId == benefit);
        }
    }
}
