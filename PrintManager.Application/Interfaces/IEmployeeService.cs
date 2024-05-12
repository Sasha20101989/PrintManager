using PrintManager.Logic.Models;

namespace PrintManager.Application.Interfaces;

public interface IEmployeeService
{
    Employee? GetById(int employeeId);

    Task<Employee?> GetByIdAsync(int employeeId);

    Task<IReadOnlyList<Employee>> GetByPageAsync(int? page, int? pageSize);
}
