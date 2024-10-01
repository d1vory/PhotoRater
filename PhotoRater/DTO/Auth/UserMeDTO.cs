namespace PhotoRater.DTO.Auth;

public class UserMeDTO
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    public int Karma { get; set; }
    
    public string Username { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    public bool EmailConfirmed { get; set; }

}