using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IHRRepository
    {
        Task<CreateEmployeeDto> CreateEployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<HRUpdateEmployeeDto> UpdateEmployeeAsync(HRUpdateEmployeeDto updateEmployeeDto);
        Task<HRUpdateEmployeeDto> FindEmployee(uint id);
    }
}
