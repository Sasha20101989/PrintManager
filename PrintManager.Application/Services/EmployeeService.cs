using PrintManager.Application.DefaultValues;
using PrintManager.Application.Interfaces;
using PrintManager.Application.Parameters;
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
        PaginationParameters paginationParameters = PaginationParameters.Create(page, pageSize, DefaultPaginationValues.EmployeeDefaultPage, DefaultPaginationValues.EmployeeDefaultPageSize);

        return await employeeStore.GetByPageAsync(paginationParameters.Skip, paginationParameters.Size);
    }
}
