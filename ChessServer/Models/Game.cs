using DataAccessLibrary.Models;

namespace ChessServer.Models;

public class Game
{

    public int Id { get; set; }
    public int FPlayer { get; set; }
    public int? SPlayer { get; set; } = null;
    public string Board { get; set; }
    public string Status { get; set; }
    public int Duration { get; set; }
    public DateTime CreatedTime { get; set; }
    public int Rating { get; set; }

    (int, string) prev;
 
    public void InitGame(GameModel game)
    {
        Id = game.Id;
        FPlayer = game.FPlayer;
        Status = game.Status;
        Duration = game.Duration;


    }

}
