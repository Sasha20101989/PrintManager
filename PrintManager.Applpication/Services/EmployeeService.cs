using PrintManager.Applpication.DefaultValues;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Applpication.Services;

public class EmployeeService(IEmployeeStore employeeStore) : IEmployeeService
{
    public async Task<IReadOnlyList<Employee>> GetByPageAsync(int? page, int? pageSize)
    {
        int pageNumber = page.HasValue && page > 0 ? page.Value : DefaultPaginationValues.EmployeeDefaultPage;
        int size = pageSize.HasValue && pageSize > 0 ? pageSize.Value : DefaultPaginationValues.EmployeeDefaultPageSize;

        int skip = (pageNumber - 1) * size;

        return await employeeStore.GetByPageAsync(skip, size);
    }
}
