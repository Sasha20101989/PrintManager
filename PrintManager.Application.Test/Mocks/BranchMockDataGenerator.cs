using PrintManager.Logic.Models;

namespace PrintManager.Application.Test.Mocks
{
    public static class BranchMockDataGenerator
    {
        public static List<Branch> GetMockBranches()
        {
            return
            [
                new Branch { BranchId = 1, BranchName = "Branch 1" },
                new Branch { BranchId = 2, BranchName = "Branch 2" },
                new Branch { BranchId = 3, BranchName = "Branch 3" }
            ];
        }
    }
}
