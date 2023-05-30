namespace ChessServer.Hubs;

public class GameManager
{
    public List<GameUser> Users = new List<GameUser>();


    public void ConnectUser(string userName, string connectionId)
    {
        GameUser userAlreadyExists = GetConnectedUserByName(userName);
        if (userAlreadyExists != null)
        {
            userAlreadyExists.AppendConnection(connectionId);
            return;
        }

        var user = new GameUser(userName);
        user.AppendConnection(connectionId);
        Users.Add(user);
    }
    
    public bool DisconnectUser(string connectionId)
    {
        GameUser? userExists = GetConnectedUserById(connectionId);
        if (userExists == null)
        {
            return false;
        }

        if (!userExists.Connections.Any())
        {
            return false; // should never happen, but...
        }

        /*bool connectionExists = userExists.Connections.Select(x => x.ConnectionId).First().Equals(connectionId);
        if (!connectionExists)
        {
            return false; // should never happen, but...
        }*/

        if (userExists.Connections.Count() == 1)
        {
            Users.Remove(userExists);
            return true;
        }

        userExists.RemoveConnection(connectionId);
        return false;
    }

    private GameUser? GetConnectedUserById(string connectionId)
    {
        foreach(var user in Users)
        {
            foreach (var con in user.Connections)
            {
                if (con.ConnectionId == connectionId) return user;
                
            }
        }
        return null;
    }
    /*=>
            Users
            .FirstOrDefault(x => x.Connections.Select(c => c.ConnectionId)
            .Contains(connectionId));
    */
    private GameUser? GetConnectedUserByName(string userName)
    {
        foreach (var user in Users)
        {
            if (user.UserName == userName) return user;
        }
        return null;
    }
        /*=>
        Users
            .FirstOrDefault(x => string.Equals(
                x.UserName,
                userName,
                StringComparison.CurrentCultureIgnoreCase));
        */
}
