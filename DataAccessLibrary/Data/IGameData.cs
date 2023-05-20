using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IGameData
    {
        Task DeleteGame(int id);
        Task<PlayerModel?> GetGame(int id);
        Task<IEnumerable<GameModel>> GetGames();
        Task InsertGame(GameModel game);
        Task UpdateGame(GameModel game);
    }
}