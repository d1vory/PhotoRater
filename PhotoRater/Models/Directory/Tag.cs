using System.ComponentModel.DataAnnotations;

namespace PhotoRater.Models.Directory;

public class Tag
{
    public int Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; }

    public List<Feedback> Feedbacks { get; set; } = [];
}