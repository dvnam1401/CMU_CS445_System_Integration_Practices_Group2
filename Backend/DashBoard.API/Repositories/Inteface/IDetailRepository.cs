using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IDetailRepository
    {
        Task<PersonalDetailDto> GetPersonalByIdAsync(decimal personalId);
        Task<IEnumerable<PersonalDetailDto>> GetAllPersonalAsync();
        Task<EmployeeDetailsDto> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeDetailsDto>> GetAllEmployeeByIdAsync(int personalId);
        Task<IEnumerable<EmployeeDetailsDto>> GetAllEmployeeAsync();
    }
}
