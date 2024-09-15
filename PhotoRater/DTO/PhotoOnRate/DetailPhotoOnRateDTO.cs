namespace PhotoRater.DTO;

public class DetailPhotoOnRateDTO
{
    public int Id { get; set; }
    public string Photo { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
}