﻿using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;
using DashBoard.API.Models.Inteface;

namespace DashBoard.API.Repositories.Inteface
{
    public interface IMysqlDataRepository
    {
        Task<IEnumerable<EmployeeMysqlDto>> FetchMysqlEmployeeDataAsync();
        //IEnumerable<EmployeeMysqlDto> FeatchMysql(IEnumerable<EmployeeMysqlDto> employees/*, IEnumerable<VacationMysqlDto> vacations*/);
        //List<EmployeeMysqlDto> FilterEmployeeDataByVacation(IEnumerable<EmployeeMysqlDto> data, EmployeeFilterDto filter);
        Task<IEnumerable<PayRate?>> GetByAllPayRate();
        Task<IEnumerable<Employee?>> GetByAllEmployee();
    }
}
