using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IEmployeeService
{
    Task<IReadOnlyList<Employee>> GetByPageAsync(int? page, int? pageSize);
}
