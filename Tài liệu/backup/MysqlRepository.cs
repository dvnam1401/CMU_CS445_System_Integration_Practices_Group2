using DashBoard.API.Data;
using DashBoard.API.Models;
using DashBoard.API.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Repositories.Implementation
{
    public class MysqlRepository : IMysqlRepository
    {
        private readonly MysqlContext dbMysqlContext;

        public MysqlRepository(MysqlContext dbMysqlContext)
        {
            this.dbMysqlContext = dbMysqlContext;
        }
        public async Task<IEnumerable<Employee?>> GetByAllEmployee()
        {
            return await dbMysqlContext.Employees.ToListAsync();
        }
    }
}
