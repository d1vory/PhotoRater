namespace PhotoRater.DTO;

public class ListPhotoOnRateDTO
{
    public int Id { get; set; }
    public string Photo { get; set; }
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }
}