using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IEditRepository
    {
        Task EditEmployeeAsync(EditEmployeeDto employeeDto);
        Task EditPersonalAsync(UpdatePersonalDto personal);
    }
}
