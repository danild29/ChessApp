using ChessServer.Data;
using ChessServer.Data.Extensions;
using ChessServer.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChessServer.Hubs.GamePlay;

public class PlayHub : Hub<IPlayHub>
{
    private readonly PlayManager _manager; 

    public readonly string TimeExceedMessage = "oponent exceeded his time limit";
    public readonly string OponentLeftMessage  = "oponent left";
    public readonly string GameCancelledMessage = "the game was cancelled";


    public PlayHub(PlayManager manager)
    {
        _manager = manager;
    }

    #region overrides

    public override async Task OnConnectedAsync()
    {
        string connectionId = Context.ConnectionId;
        GameInfo? game = _manager.ConnectGamer(connectionId);

        DebugClass.Strings.Add("user play " + Context.ConnectionId);


        await base.OnConnectedAsync();
        if(game != null)
        {
            game.GameStarted = DateTime.Now;
            game.LastMoveTime = DateTime.Now;
            string gameJsonString = JsonConvert.SerializeObject(game);

            await Clients.Client(game.White).StartGame(game.Black, Sides.White, gameJsonString);
            await Clients.Client(game.Black).StartGame(game.White, Sides.Black, gameJsonString);
        }
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string connectionId = Context.ConnectionId;
        GameInfo? gameRemoved = _manager.DisconnectGamer(connectionId);

        DebugClass.Strings.Remove("user play " + Context.ConnectionId);


        if (gameRemoved != null)
        {
            MessageInfo message = new(true, OponentLeftMessage);
            message.FromGamer = connectionId;
            message.ToGamer = gameRemoved.GetEnemy(connectionId);

            if (message.ToGamer == null)
            {
                DebugClass.Strings.Add("weellll its nuuuuul");
            }
            else
            {
                string messageJson = JsonConvert.SerializeObject(message);
                await Clients.Client(message.ToGamer).SendMessageToPlayer(messageJson);
            }
            
        }

        await base.OnDisconnectedAsync(exception);
    }


    #endregion

    public async Task SendMove(int boardId, string moveStr)
    {
        GameInfo? game = _manager.Games.FirstOrDefault(x => x.Id == boardId);
        Move? move = JsonConvert.DeserializeObject<Move>(moveStr);

        if (game == null || move == null) return;

        if (game.IsGameOver) DebugClass.Strings.Add("why are you still playing");



        if(game.CurrentMove == Sides.White)
        {
            game.WhitePlayed += (DateTime.Now - game.LastMoveTime).TotalSeconds;
        }
        if (game.CurrentMove == Sides.Black)
        {
            game.BlackPlayed += (DateTime.Now - game.LastMoveTime).TotalSeconds;
        }

        game.LastMoveTime = DateTime.Now;
        game.Moves.Add(move.ToString());
        game.MakeMove(move);

        game.LastMoveTime = DateTime.Now;



        string? enenmyConnection = game.GetEnemy(Context.ConnectionId); 
        if(enenmyConnection != null)
        {
            string  gameJsonString = JsonConvert.SerializeObject(game);
            await Clients.Client(enenmyConnection).WasMove(gameJsonString);
        }
        else
            DebugClass.Strings.Add("say there were a move to user: NULL");
    }

    public async Task DefineWinner(int boardId)
    {
        GameInfo? game = _manager.Games.FirstOrDefault(x => x.Id == boardId);
        if (game == null ) return;

        if(game.TimeLeftBlack > 0 && game.TimeLeftWhite > 0)
        {
            DebugClass.Strings.Add("shiiiit");
        }


        if(game.Moves.Count < 2)
        {
            MessageInfo cancelledMessage = new(true, GameCancelledMessage);
            cancelledMessage.FromGamer = "Server";
            string cancelledMessageJson = JsonConvert.SerializeObject(cancelledMessage);

            await Clients.Client(game.Black).SendMessageToPlayer(cancelledMessageJson);
            await Clients.Client(game.White).SendMessageToPlayer(cancelledMessageJson);
            return;
        }

        if (game.CurrentMove == Sides.White)
        {
            game.Winner = game.Black;
        }
        if (game.CurrentMove == Sides.Black)
        {
            game.Winner = game.White;
        }

        MessageInfo message = new(true, TimeExceedMessage);
        message.ToGamer = game.Winner;
        message.FromGamer = game.GetEnemy(message.ToGamer);

        string messageJson = JsonConvert.SerializeObject(message);
        await Clients.Client(message.ToGamer).SendMessageToPlayer(messageJson);
    }
 }


public class Move
{
    public int Fromx;
    public int Fromy;
    public int Tox;
    public int Toy;


    public Move()
    {

    }
 

    public override string ToString()
    {
        return $"{Fromy + 1}{(char)(Fromx + 97)} -> {Toy + 1}{(char)(Tox + 97)}";
    }
}



public class MessageInfo
{
    public DateTime SendAt;
    public bool IsStopGame;
    public string Message;
    public string FromGamer;
    public string? ToGamer;

    public MessageInfo(bool isStop, string message)
    {
        IsStopGame = isStop;
        Message = message;
        SendAt = DateTime.UtcNow;
    }

}
public interface IPlayHub
{
    Task StartGame(string message, Sides side, string game);
    Task SendMessageToPlayer(string message);
    Task WasMove(string game);
}

