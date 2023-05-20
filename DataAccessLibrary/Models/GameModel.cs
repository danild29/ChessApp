
namespace DataAccessLibrary.Models;

public class GameModel
{
    public int Id { get; set; }
    public int FPlayer { get; set; }
    public int? SPlayer { get; set; } = null;
    public string Board { get; set; }
    public string Status { get; set; }
    public int Duration { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public int Rating { get; set; }
}
