namespace PrintManager.Logic.Models;

public class Branch
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public string? Location { get; set; } = null!;
}
