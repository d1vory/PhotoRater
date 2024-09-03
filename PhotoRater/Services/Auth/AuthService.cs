using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PhotoRater.Areas.Identity.Data;
using PhotoRater.DTO.Auth;

namespace PhotoRater.Services.Auth;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly BaseApplicationContext _db;
    
    public AuthService(BaseApplicationContext db, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
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
}