using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface ICreatedRepository
    {
        Task CreatePersonalAsync(CreatePersonalDto createPersonal);
        Task CreateEmployeeAsync(CreateEmployeeDto employment);
    }
}
