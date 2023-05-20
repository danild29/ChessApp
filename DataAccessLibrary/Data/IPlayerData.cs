using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data;

public interface IPlayerData
{
    Task<PlayerModel?> GetPlayer(int id);
    Task<PlayerModel?> GetPlayer(string Email);
    Task<PlayerModel?> AuthorizePlayer(string Email, string Pasword);
    Task<IEnumerable<PlayerModel>> GetPlayers();
    Task InsertPlayer(PlayerModel player);
}