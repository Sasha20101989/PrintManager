using PrintManager.Logic.Models;

namespace PrintManager.Application.DTOs;

public class StatusDTO
{
    private StatusDTO(Status status)
    {
        StatusId = status.StatusId;
        StatusName = status.StatusName;
    }

    public int StatusId { get; set; }

    public string StatusName { get; set; }

    public static StatusDTO Create(Status status)
    {
        return new StatusDTO(status);
    }
}
