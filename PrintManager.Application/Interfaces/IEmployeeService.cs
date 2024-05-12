using PrintManager.Logic.Models;

namespace PrintManager.Application.Interfaces;

public interface IEmployeeService
{
    Task<Employee?> GetByIdAsync(int employeeId);

    Task<IReadOnlyList<Employee>> GetByPageAsync(int? page, int? pageSize);
}
