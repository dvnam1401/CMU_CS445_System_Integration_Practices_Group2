﻿using DashBoard.API.Models.Domain;
using DashBoard.API.Models.DTO;

namespace DashBoard.API.Repositories.Inteface
{
    public interface ISqlDataRepository
    {
        List<string> GetDepartments();
        Task<IEnumerable<BenefitPlan?>> GetByAllBenefitPlan();
        Task<BenefitPlan?> GetBenefitPlanById(decimal BenefitPlansId);
        Task<IEnumerable<EmploymentDto?>> FetchEmployments();
        Task<IEnumerable<EmploymentDto>> FetchPersonal();
        Task<IEnumerable<EmploymentWorkingTimeDto>> FetchWorkingTimes(int minimumDays);
        Task<IEnumerable<EmploymentSqlServerDto>> FetchSqlServerData(EmployeeFilterDto filter);
        IEnumerable<EmploymentSqlServerDto> FilterJobHistorySqlServe(IEnumerable<EmploymentSqlServerDto> data, EmployeeFilterDto filter);
    }
}
