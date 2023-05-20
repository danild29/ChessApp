namespace ChessServer.Hubs;

public class GameUser
{

    public string UserName { get; set; } = null!;

    private readonly List<GameConnection> _connections;
    public IEnumerable<GameConnection> Connections => _connections;

    public GameUser(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        _connections = new List<GameConnection>();
    }

    public DateTime? ConnectedAt
    {
        get
        {
            if (Connections.Any())
            {
                return Connections
                    .OrderByDescending(x => x.ConnectedAt)
                    .Select(x => x.ConnectedAt)
                    .First();
            }

            return null;
        }
    }


    public void AppendConnection(string connectionId)
    {
        if (connectionId == null)
        {
            throw new ArgumentNullException(nameof(connectionId));
        }

        var connection = new GameConnection
        {
            ConnectedAt = DateTime.UtcNow,
            ConnectionId = connectionId
        };

        _connections.Add(connection);
    }

    public void RemoveConnection(string connectionId)
    {
        if (connectionId == null)
        {
            throw new ArgumentNullException(nameof(connectionId));
        }

        var connection = _connections.SingleOrDefault(x => x.ConnectionId.Equals(connectionId));
        if (connection == null)
        {
            return;
        }
        _connections.Remove(connection);
    }



}
