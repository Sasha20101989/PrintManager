using PrintManager.Logic.Models;

namespace PrintManager.Application.Test.Mocks;

public static class BranchMockDataGenerator
{
    public static List<Branch> GetMockBranches()
    {
        return
        [
            new Branch { BranchId = 1, BranchName = "Тридевятое царство" },
            new Branch { BranchId = 2, BranchName = "Дремучий Лес" },
            new Branch { BranchId = 3, BranchName = "Луна" }
        ];
    }
}
