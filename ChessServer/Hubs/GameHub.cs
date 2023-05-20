using ChessServer.Data;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.SignalR;
using System;

namespace ChessServer.Hubs;

public class GameHub: Hub<IGameHub>
{
    private readonly GameManager _gameManager;
    private PlayerModel _player;
    private const string _defaultGroupName = "General";

    public GameHub(GameManager  manager, PlayerModel player)
    {
        _gameManager = manager;
        _player = player;
    }

    #region overrides

    public override async Task OnConnectedAsync()
    {
        DebugClass.Strings.Add(Context.ConnectionId);


        var id = Context?.GetHttpContext()?.GetRouteValue("Id") as string;
        string userName =  id ?? "Anon";
        string connectionId = Context.ConnectionId;
        _gameManager.ConnectUser(userName, connectionId);
        await Groups.AddToGroupAsync(connectionId, userName);
        await UpdateUsersAsync();
        await base.OnConnectedAsync();
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        DebugClass.Strings.Remove(Context.ConnectionId);

        bool isUserRemoved = _gameManager.DisconnectUser(Context.ConnectionId);
        if (isUserRemoved)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _defaultGroupName);
            await UpdateUsersAsync();

        }
        await base.OnDisconnectedAsync(exception);
    }


    #endregion

    


    public async Task UpdateUsersAsync()
    {
        List<string> res = new();
        _gameManager.Users.ForEach(x => x.Connections.ToList().ForEach(y => res.Add(y.ConnectionId)));
        
        await Clients.All.UpdateUsersAsync(res);
    }

    public async Task SendMessageAsync(string groupName, string message)
    {
        if (message == "disc") Context.Abort();
        else
            await Clients.Group(groupName).SendMessageAsync(groupName, message);
    }


    /*public Task SendChangedGame(string board, string change)
    {
        return Clients.All.SendAsync("RecieveCHanges", board, change);
    }*/
}

public interface IGameHub
{
    Task UpdateUsersAsync(IEnumerable<string> users);

    Task SendMessageAsync(string userName, string message);
}