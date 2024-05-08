namespace PrintManager.Logic.Models;

public class ConnectionType
{
    private ConnectionType(int id, string typeName)
    {
        Id = id;
        TypeName = typeName;
    }

    public int Id { get; private set; }

    public string TypeName { get; private set; }

    public static ConnectionType Create(int id, string typeName)
    {
        return new ConnectionType(id, typeName);
    }
}
