using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Applpication.Services;

public class EmployeeService(IEmployeeStore employeeStore) : IEmployeeService
{
    public async Task<IReadOnlyList<Employee>> GetAllAsync()
    {
        return await employeeStore.GetAllAsync();
    }
}
