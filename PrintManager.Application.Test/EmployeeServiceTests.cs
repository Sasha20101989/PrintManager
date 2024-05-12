using Moq;
using PrintManager.Application.Services;
using PrintManager.Application.Test.Mocks;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Application.Test;

public class EmployeeServiceTests
{
    [Fact]
    public void GetById_WithValidId_ReturnsEmployee()
    {
        // Arrange
        int employeeId = 1;

        Employee expectedEmployee = EmployeeMockDataGenerator.GetMockEmployees().First();

        Mock<IEmployeeStore> mockEmployeeStore = new();

        mockEmployeeStore.Setup(store => store.GetById(employeeId)).Returns(expectedEmployee);

        EmployeeService employeeService = new(mockEmployeeStore.Object);

        // Act
        Employee? result = employeeService.GetById(employeeId);

        // Assert
        Assert.Equal(expectedEmployee, result);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsEmployee()
    {
        // Arrange
        int employeeId = 1;

        Employee expectedEmployee = EmployeeMockDataGenerator.GetMockEmployees().First();

        Mock<IEmployeeStore> mockEmployeeStore = new();

        mockEmployeeStore.Setup(store => store.GetByIdAsync(employeeId)).ReturnsAsync(expectedEmployee);

        EmployeeService employeeService = new(mockEmployeeStore.Object);

        // Act
        Employee? result = await employeeService.GetByIdAsync(employeeId);

        // Assert
        Assert.Equal(expectedEmployee, result);
    }

    [Fact]
    public async Task GetByPageAsync_NoPaginationParameters_ReturnsAllEmployees()
    {
        // Arrange
        Mock<IEmployeeStore> mockEmployeeStore = new();

        List<Employee> expectedEmployees = EmployeeMockDataGenerator.GetMockEmployees();

        mockEmployeeStore.Setup(x => x.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedEmployees);

        EmployeeService employeeService = new(mockEmployeeStore.Object);

        // Act
        IReadOnlyList<Employee> result = await employeeService.GetByPageAsync(null, null);

        // Assert
        Assert.Equal(expectedEmployees.Count, result.Count);

        foreach (var employee in expectedEmployees)
        {
            Assert.Contains(employee, result);
        }
    }

    [Fact]
    public async Task GetByPageAsync_WithPaginationParameters_ReturnsRequestedEmployees()
    {
        // Arrange
        Mock<IEmployeeStore> mockEmployeeStore = new();

        List<Employee> expectedEmployees = EmployeeMockDataGenerator.GetMockEmployees().Take(2).ToList();

        mockEmployeeStore.Setup(x => x.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedEmployees);

        EmployeeService employeeService = new(mockEmployeeStore.Object);

        // Act
        int page = 1;

        int pageSize = 2;

        IReadOnlyList<Employee> result = await employeeService.GetByPageAsync(page, pageSize);

        // Assert
        Assert.Equal(pageSize, result.Count);

        foreach (var employee in expectedEmployees.GetRange((page - 1) * pageSize, pageSize))
        {
            Assert.Contains(employee, result);
        }
    }

    [Fact]
    public async Task GetByPageAsync_PageGreaterThanTotalPages_ReturnsEmptyList()
    {
        // Arrange
        Mock<IEmployeeStore> mockEmployeeStore = new();

        List<Employee> expectedEmployees = [];

        mockEmployeeStore.Setup(x => x.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedEmployees);

        EmployeeService employeeService = new(mockEmployeeStore.Object);

        // Act
        int page = 100;

        int pageSize = 10;

        IReadOnlyList<Employee> result = await employeeService.GetByPageAsync(page, pageSize);

        // Assert
        Assert.Empty(result);
    }
}
