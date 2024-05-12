using PrintManager.Logic.Models;

namespace PrintManager.Application.RDOs
{
    public class EmployeeRDO
    {
        private EmployeeRDO(int id, string employeeName, Branch branch)
        {
            EmployeeId = id;
            EmployeeName = employeeName;
            Branch = branch;
        }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = null!;

        public virtual Branch Branch { get; set; } = null!;

        public static EmployeeRDO Create(int id, string employeeName, Branch branch)
        {
            return new EmployeeRDO(id, employeeName, branch);
        }
    }
}
