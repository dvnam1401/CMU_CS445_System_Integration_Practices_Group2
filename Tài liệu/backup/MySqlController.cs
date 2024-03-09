using DashBoard.API.Repositories.Implementation;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MySqlController : ControllerBase
    {
        private readonly IMysqlRepository mysqlRepository;
        public MySqlController(IMysqlRepository mysqlRepository)
        {
            this.mysqlRepository = mysqlRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var result = await mysqlRepository.GetByAllEmployee();
            return Ok(result);
        }
    }


}
