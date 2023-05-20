using DataAccess.DbAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data;

public class GameData : IGameData
{
    private readonly ISqlDataAccess _db;

    public GameData(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task<IEnumerable<GameModel>> GetGames()
    {
        string sql = @"SELECT * FROM dbo.[Games]";

        return _db.LoadData<GameModel, dynamic>(sql, new { });
    }

    public async Task<PlayerModel?> GetGame(int id)
    {
        string sql = @"SELECT * FROM dbo.[Players] WHERE @Id=Id";
        var res = await _db.LoadData<PlayerModel, dynamic>(sql, new { Id = id });

        return res.FirstOrDefault();
    }

    public async Task InsertGame(GameModel game)
    {
        string sql = @"INSERT INTO dbo.[Games] (FPlayer, SPlayer, Board, Status)
                    values (@FPlayer, @SPlayer, @Board, @Status);";

        await _db.SaveData(sql, new { game.FPlayer, game.SPlayer, game.Board, game.Status });
    }


    public async Task UpdateGame(GameModel game)
    {
        string sql = @"Update dbo.[Pair]
	                   set SPlayer = @SPlayer, Board = @Board, Status = @Status
	                   where Id = @Id;";
        await _db.SaveData(sql, new { game.SPlayer, game.Board, game.Status, game.Id });
    }

    public Task DeleteGame(int id) => _db.SaveData(@"DELETE FROM dbo.[Game] where Id = @Id;", new { Id = id });

}
