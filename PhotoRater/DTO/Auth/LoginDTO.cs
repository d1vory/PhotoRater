using System.ComponentModel.DataAnnotations;

namespace PhotoRater.DTO.Auth;

public class LoginDTO
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public class RefreshTokenDTO
{
    [Required]
    public string RefreshToken { get; set; }
}