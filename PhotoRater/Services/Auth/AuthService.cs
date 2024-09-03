using Microsoft.AspNetCore.Identity;
using PhotoRater.Areas.Identity.Data;

namespace PhotoRater.Services.Auth;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly BaseApplicationContext _db;
    
    public AuthService(BaseApplicationContext db)
    {
        _db = db;
    }
}