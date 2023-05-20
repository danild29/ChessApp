using System.ComponentModel.DataAnnotations;

namespace ChessServer.Models;

public class DisplayPlayerModel
{
    [MinLength(1, ErrorMessage ="u must have a name")]
    [StringLength(50, ErrorMessage ="too long")]
    public string? NickName { get; set; }

    [MinLength(1, ErrorMessage = "u must have a password")]
    [StringLength(50, ErrorMessage = "too long password")]
    public string? Pasword { get; set; }


    [Required]
    [EmailAddress]
    [MaxLength(50, ErrorMessage ="bad email")]
    public string? Email { get; set; }
    
    [Required]
    [Range(0, 100000)]
    public int Rating { get; set; }

    public bool IsPremium { get; set; }

    [StringLength(100, ErrorMessage = "too long")]
    public string? Status { get; set; }
}
