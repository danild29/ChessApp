namespace DataAccessLibrary.Models;


public class PlayerModel
{
    public int? Id { get; set; }
    public string? NickName { get; set; }
    public string? Email { get; set; }
    public string? Pasword {get; set;}
    public int Rating { get; set; } = -1;
    public bool IsPremium { get; set; } = false;
    public string? Status { get; set; }
}


