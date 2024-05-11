using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IEmployeeService
{
    Task<Employee?> GetByIdAsync(int employeeId);

    Task<IReadOnlyList<Employee>> GetByPageAsync(int? page, int? pageSize);
}
