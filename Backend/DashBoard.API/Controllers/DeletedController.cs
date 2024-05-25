using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeletedController : ControllerBase
    {
        private readonly IDeletedRepository deletedRepository;
        public DeletedController(IDeletedRepository deletedRepository)
        {
            this.deletedRepository = deletedRepository;
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            //try
            //{
                await deletedRepository.DeleteEmployeeAsync(id);
                return Ok(/*$"Employee with ID {id} has been deleted successfully."*/);
            //}
            //catch (KeyNotFoundException ex)
            //{
            //    return NotFound(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Internal server error: {ex.Message}");
            //}
        }

        [HttpDelete("DeletePersonal/{id}")]
        public async Task<IActionResult> DeletePersonal(decimal id)
        {
            try
            {
                await deletedRepository.DeletePersonalAsync(id);
                return Ok(/*$"Employee with ID {id} has been deleted successfully."*/);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
