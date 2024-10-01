using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HttpExceptions.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotoRater.Models;
using PhotoRater.DTO.Auth;

namespace PhotoRater.Services.Auth;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly BaseApplicationContext _db;
    protected readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    
    public AuthService(BaseApplicationContext db, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<bool> RegisterUser(RegisterUserDTO dto,  ModelStateDictionary modelState)
    {
        var user = _mapper.Map<User>(dto);
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
        //_signInManager.AuthenticationScheme =  IdentityConstants.BearerScheme;
        var signInResult = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, true);
        if (!signInResult.Succeeded) throw new BadRequestException("Wrong username or password!");
        var token = GenerateAccessToken(dto.Username);
        return token;

    }

    public async Task<JwtSecurityToken> RefreshLogin(RefreshTokenDTO dto)
    {
        var refreshToken = await _db.RefreshTokens
            .Include(refreshToken => refreshToken.User)
            .FirstOrDefaultAsync(r => r.Token == dto.RefreshToken);
        if (refreshToken == null) throw new BadRequestException("Refresh token is not found!");
        var token = GenerateAccessToken(refreshToken.User.UserName);
        _db.Remove(refreshToken);
        await _db.SaveChangesAsync();
        return token;
    }

    public async Task<string> GetRefreshToken(string username)
    {
        var refreshTokenGuid = Guid.NewGuid().ToString();
        var expireIn = DateTime.Now + TimeSpan.FromHours(4);
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) throw new BadRequestException("User is not found!");
        var refreshToken = new RefreshToken() { Token = refreshTokenGuid, UserId = user.Id, Expire = expireIn };
        await _db.RefreshTokens.AddAsync(refreshToken);
        await _db.SaveChangesAsync();
        return refreshTokenGuid;
    }
    
    
    private JwtSecurityToken GenerateAccessToken(string username)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            // Add additional claims as needed (e.g., roles, etc.)
        };
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