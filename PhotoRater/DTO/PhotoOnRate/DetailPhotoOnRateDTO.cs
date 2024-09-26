namespace PhotoRater.DTO;

public class DetailPhotoOnRateDTO
{
    public int Id { get; set; }
    public string Photo { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public double AverageRating { get; set; }
    public int MaxRating { get; set; }
    public int MinRating { get; set; }
    
    public string[] Comments { get; set; }
    
}