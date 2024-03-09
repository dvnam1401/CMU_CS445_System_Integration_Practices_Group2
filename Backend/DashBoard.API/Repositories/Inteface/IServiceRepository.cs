using DashBoard.API.Models;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IServiceRepository
    {
        Task<BenefitPlan?> GetById(int benefit);
        Task<IEnumerable<BenefitPlan?>> GetByAll();
    }
}
