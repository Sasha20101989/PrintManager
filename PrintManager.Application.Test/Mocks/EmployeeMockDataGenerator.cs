using PrintManager.Logic.Models;

namespace PrintManager.Application.Test.Mocks;

public static class EmployeeMockDataGenerator
{
    public static List<Employee> GetMockEmployees()
    {
        return
        [
            new Employee { EmployeeId = 1, EmployeeName = "Царь", BranchId = 1 },
            new Employee { EmployeeId = 2, EmployeeName = "Яга", BranchId = 2 },
            new Employee { EmployeeId = 3, EmployeeName = "Копатыч", BranchId = 3 },
            new Employee { EmployeeId = 4, EmployeeName = "Добрыня", BranchId = 1 },
            new Employee { EmployeeId = 5, EmployeeName = "Кощей", BranchId = 3 },
            new Employee { EmployeeId = 6, EmployeeName = "Лосяш", BranchId = 3 },
        ];
    }
}
