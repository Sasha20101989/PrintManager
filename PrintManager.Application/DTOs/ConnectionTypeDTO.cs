using PrintManager.Logic.Models;

namespace PrintManager.Application.DTOs;

public class ConnectionTypeDTO
{
    private ConnectionTypeDTO(ConnectionType connectionType)
    {
        ConnectionTypeId = connectionType.ConnectionTypeId;

        ConnectionTypeName = connectionType.ConnectionTypeName;
    }

    public int ConnectionTypeId { get; }

    public string ConnectionTypeName { get; } = null!;

    public static ConnectionTypeDTO Create(ConnectionType connectionType)
    {
        return new ConnectionTypeDTO(connectionType);
    }
}
