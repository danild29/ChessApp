using DataAccess.DbAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data;

public class PlayerData : IPlayerData
{
    private readonly ISqlDataAccess _db;

    public PlayerData(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task<IEnumerable<PlayerModel>> GetPlayers()
    {
        string sql = @"SELECT * FROM dbo.[Players]";

        return _db.LoadData<PlayerModel, dynamic>(sql, new { });
    }

    public async Task<PlayerModel?> GetPlayer(int id)
    {
        string sql = @"SELECT * FROM dbo.[Players] WHERE @Id=Id";
        var res = await _db.LoadData<PlayerModel, dynamic>(sql, new { Id = id });

        return res.FirstOrDefault();
    }

    public async Task<PlayerModel?> GetPlayer(string Email)
    {
        string sql = @"SELECT * FROM dbo.[Players] WHERE @Email=Email";
        var res = await _db.LoadData<PlayerModel, dynamic>(sql, new { Email});

        PlayerModel? output = res.FirstOrDefault();
        
        if(output is not null)
        {
            output.Pasword = string.Empty;
        }

        return output;
    }
    
    public async Task<PlayerModel?> AuthorizePlayer(string Email, string Pasword)
    {
        string sql = @"SELECT * FROM dbo.[Players] WHERE @Email=Email and @Pasword=Pasword";
        var res = await _db.LoadData<PlayerModel, dynamic>(sql, new { Email, Pasword });

        return res.FirstOrDefault();
    }

    public async Task InsertPlayer(PlayerModel player)
    {
        string sql = @"INSERT INTO dbo.[Players] (NickName, Email, Pasword)
                    values (@NickName, @Email, @Pasword);";

        await _db.SaveData(sql, new { player.NickName, player.Email, player.Pasword });

        /*var newId = await _db.LoadData<int, dynamic>(sql, new { player.NickName, player.Email, player.Pasword });
         return newId.FirstOrDefault();*/
    }

    /*
    public Task UpdatePlayer(PlayerModel player)
    {
        string sql = @"INSERT INTO dbo.[Players] (NickName, Email, Pasword)
                    values (@NickName, @Email, @Pasword);";
        _db.SaveData("dbo.spPlayer_Update", player);
    }*/

    //public Task Deleteplayer(int id) => _db.SaveData("dbo.spPlayer_Delete", new { Id = id });

}
