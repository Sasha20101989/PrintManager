using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Persistence.Repositories;

public class EmployeeRepository(PrintingManagementContext context, IMapper mapper) : IEmployeeStore
{
    public async Task<IReadOnlyList<Employee>> GetAllAsync()
    {
        IReadOnlyList<Employee> employees = await context.Employees
            .AsNoTracking()
            .Select(e => mapper.Map<Employee>(e))
            .ToListAsync();

        return employees;
    }

    public async Task<IReadOnlyList<Employee>> GetByPageAsync(int skip, int pageSize)
    {
        return await context.Employees
            .AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .Select(e => mapper.Map<Employee>(e))
            .ToListAsync();
    }
}
