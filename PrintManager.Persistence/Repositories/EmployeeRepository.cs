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
            .Select(b => Employee.Create(b.EmployeeId, b.Name))
            .ToListAsync();

        return employees;
    }
}
