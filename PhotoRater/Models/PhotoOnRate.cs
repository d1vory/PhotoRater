using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoRater.Models;

public class PhotoOnRate
{
    public int Id { get; set; }
    
    [StringLength(450)]
    public string UserId { get; set; } = null!;
    
    [ForeignKey("UserId")]
    public User User { get; set; }  = null!;
    
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // public DateTime CreatedAt { get; set; }
    
    [StringLength(1000)]
    public string Photo { get; set; }

    [StringLength(250)]
    public string Name { get; set; }
    
    public string Description { get; set; } = "";
    
    public DateTime CreatedAt
    {
        get => _createdAt ?? DateTime.Now;
        set => _createdAt = value;
    }

    private DateTime? _createdAt;
}