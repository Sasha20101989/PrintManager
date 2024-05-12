using PrintManager.Application.DefaultValues;
using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Application.Services;

public class EmployeeService(IEmployeeStore employeeStore) : IEmployeeService
{
    public Employee? GetById(int employeeId)
    {
        return employeeStore.GetById(employeeId);
    }

    public async Task<Employee?> GetByIdAsync(int employeeId)
    {
        return await employeeStore.GetByIdAsync(employeeId);
    }

    public async Task<IReadOnlyList<Employee>> GetByPageAsync(int? page, int? pageSize)
    {
        int pageNumber = page.HasValue && page > 0 ? page.Value : DefaultPaginationValues.EmployeeDefaultPage;
        int size = pageSize.HasValue && pageSize > 0 ? pageSize.Value : DefaultPaginationValues.EmployeeDefaultPageSize;

        int skip = (pageNumber - 1) * size;

        return await employeeStore.GetByPageAsync(skip, size);
    }
}
