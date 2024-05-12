using PrintManager.Logic.Models;

namespace PrintManager.Application.DTOs
{
    public class EmployeeDTO
    {
        private EmployeeDTO(Employee employee, BranchDTO branch)
        {
            EmployeeId = employee.EmployeeId;
            EmployeeName = employee.EmployeeName;
            Branch = branch;
        }

        public int EmployeeId { get; }

        public string EmployeeName { get; } = null!;

        public virtual BranchDTO Branch { get; } = null!;

        public static EmployeeDTO Create(Employee employee)
        {
            BranchDTO branchDTO = BranchDTO.Create(employee.Branch);

            return new EmployeeDTO(employee, branchDTO);
        }
    }
}
