
using System.ComponentModel.DataAnnotations;

namespace PhotoRater.DTO.Auth;

public class RegisterUserDTO
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password", ErrorMessage = "Passwords dont match")]
    [DataType(DataType.Password)]
    public string Password2 { get; set; }
}