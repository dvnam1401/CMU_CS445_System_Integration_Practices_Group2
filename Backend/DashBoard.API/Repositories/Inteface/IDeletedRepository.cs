namespace DashBoard.API.Repositories.Inteface
{
    public interface IDeletedRepository
    {
        Task DeleteEmployeeAsync(int employeeId);
        Task DeletePersonalAsync(decimal personalId);
    }
}
