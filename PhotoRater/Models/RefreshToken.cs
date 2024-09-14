
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoRater.Models;

public class RefreshToken
{
    public int Id { get; set; }
    [StringLength(1000)]
    public string Token { get; set; } = null!;
    public DateTime Expire { get; set; }
    
    [StringLength(450)]
    public string UserId { get; set; } = null!;
    [ForeignKey("UserId")]
    public User User { get; set; }  = null!;

}