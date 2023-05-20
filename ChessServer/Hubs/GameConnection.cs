namespace ChessServer.Hubs;

public class GameConnection
{
    public DateTime ConnectedAt { get; set; }
    public string ConnectionId { get; set; } = null!;
}
