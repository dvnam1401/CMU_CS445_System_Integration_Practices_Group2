using DashBoard.API.Models;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IMysqlRepository
    {
        Task<IEnumerable<Employee?>> GetByAllEmployee();

    }
}
