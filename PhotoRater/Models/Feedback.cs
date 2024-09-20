using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoRater.Models;

public class Feedback
{
    public int Id { get; set; }
    
    [StringLength(450)]
    public string? ReviewerId { get; set; }
    
    [ForeignKey("ReviewerId")]
    public User? Reviewer { get; set; }
    
    public int PhotoOnRateId { get; set; }
    
    [ForeignKey("PhotoOnRateId")]
    public PhotoOnRate PhotoOnRate { get; set; }  = null!;
    
    [Range(0, 10)]
    public int DigitalRating { get; set; }
    
    public string Comment { get; set; } = "";
}