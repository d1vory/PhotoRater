using System.ComponentModel.DataAnnotations;

namespace PhotoRater.Models.Directory;


public class Status
{
    public enum Values:int
    {
        OnReview=1,
        Inactive=2,
        Reviewed=3,
        Deleted=4
    }
    public int Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; }
    
    
}