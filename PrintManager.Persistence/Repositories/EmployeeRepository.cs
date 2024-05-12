using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Entities;

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

    public Employee? GetById(int employeeId)
    {
        EmployeeEntity? employee =context.Employees
            .AsNoTracking()
            .Include(e => e.Branch)
            .FirstOrDefault(i => i.EmployeeId == employeeId);

        return mapper.Map<Employee>(employee);
    }

    public async Task<Employee?> GetByIdAsync(int employeeId)
    {
        EmployeeEntity? employee = await context.Employees
            .AsNoTracking()
            .Include(e => e.Branch)
            .FirstOrDefaultAsync(i => i.EmployeeId == employeeId);

        return mapper.Map<Employee>(employee);
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
