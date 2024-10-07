using System.ComponentModel.DataAnnotations;

namespace PhotoRater.Utils.Feedback;

public class CreateFeedbackDTO
{
    [Range(0, 10)]
    public int DigitalRating { get; set; }
    
    public string Comment { get; set; } = "";

    public int[] Tags { get; set; } = [];
}