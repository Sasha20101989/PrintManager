using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IEmployeeStore
{
    Task<IReadOnlyList<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(int employeeId);

    Task<IReadOnlyList<Employee>> GetByPageAsync(int skip, int pageSize);
}
