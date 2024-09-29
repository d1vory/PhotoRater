using System.ComponentModel.DataAnnotations;

namespace PhotoRater.Models.Directory;

public class Status
{
    public int Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; }
    
    
}