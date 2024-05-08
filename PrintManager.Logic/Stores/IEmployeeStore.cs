using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IEmployeeStore
{
    Task<IReadOnlyList<Employee>> GetAllAsync();
}
