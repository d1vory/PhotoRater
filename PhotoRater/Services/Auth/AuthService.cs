using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using PhotoRater.Areas.Identity.Data;
using PhotoRater.DTO.Auth;

namespace PhotoRater.Services.Auth;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly BaseApplicationContext _db;
    protected readonly IConfiguration _configuration;
    
    public AuthService(BaseApplicationContext db, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<bool> RegisterUser(RegisterUserDTO dto,  ModelStateDictionary modelState)
    {
        var user = new User() { Email = dto.Email, UserName = dto.Username };
        var res = await _userManager.CreateAsync(user, dto.Password);
        if (res.Succeeded)
        {
            return true;
        }

        foreach (var error in res.Errors)
        {
            modelState.AddModelError(error.Code, error.Description);
        }

        return false;
    }

    public async Task<JwtSecurityToken> Login(LoginDTO dto, ModelStateDictionary modelState)
    {
        _signInManager.AuthenticationScheme =  IdentityConstants.BearerScheme;
        var signInResult = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, true);
        if (!signInResult.Succeeded) throw new ApplicationException("Wrong username or password!");
        var token = GenerateAccessToken(dto.Username);
        return token;

    }
    
    
    private JwtSecurityToken GenerateAccessToken(string username)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            // Add additional claims as needed (e.g., roles, etc.)
        };
        var a = _configuration["JwtSettings:Issuer"];
        var b = _configuration["JwtSettings:Audience"];
        var c = _configuration["JwtSettings:SecretKey"];

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(120), // Token expiration time
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
}