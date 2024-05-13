using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IEditRepository
    {
        Task EditEmployeeAsync(EditEmployeeDto employeeDto);
    }
}
