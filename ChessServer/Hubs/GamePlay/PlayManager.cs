using ChessServer.Data;
using ChessServer.Models;
using ChessServer.Pages;

namespace ChessServer.Hubs.GamePlay;


public class PlayManager
{
    public List<GameInfo> Games = new List<GameInfo>();

    private int IdGiver = 4;
    public GameInfo? ConnectGamer(string connectionId)
    {
        var freeGame = FindFreeGame();
        if(freeGame != null)
        {
            if (freeGame.White == null) freeGame.White = connectionId;
            else if (freeGame.Black == null) freeGame.Black = connectionId;
            
            return freeGame;
        }
        else
        {
            GameInfo gameInfo = new(IdGiver, connectionId);
            IdGiver += 6;
            Games.Add(gameInfo);

            return null;
        }

    }


    public GameInfo? DisconnectGamer(string connectionId)
    {
        GameInfo? gameExists = GetConnectedGameById(connectionId);
        if (gameExists == null)
        {
            return null;
        }

        if(gameExists.Moves.Count == 0)
        {
            if (gameExists.White == connectionId) gameExists.White = null;
            if (gameExists.Black == connectionId) gameExists.Black = null;

            if(gameExists.Black == null && gameExists.White == null) Games.Remove(gameExists);
            return null;
        }

        Games.Remove(gameExists);
        return gameExists;
    }

    private GameInfo? GetConnectedGameById(string connectionId)
    {
        return Games.FirstOrDefault(g => g.White == connectionId || g.Black == connectionId);
    }
    
    private GameInfo? FindFreeGame()
    {
        return Games.FirstOrDefault(g => g.White == null || g.Black == null);
    }
    
}
