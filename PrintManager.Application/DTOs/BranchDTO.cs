using PrintManager.Logic.Models;

namespace PrintManager.Application.DTOs;

public class BranchDTO
{
    private BranchDTO(Branch branch)
    {
        BranchId = branch.BranchId;
        BranchName = branch.BranchName;
        Location = branch.Location;
    }

    public int BranchId { get;}

    public string BranchName { get;} = null!;

    public string? Location { get;} = null!;

    public static BranchDTO Create(Branch branch)
    {
        return new BranchDTO(branch);
    }
}
