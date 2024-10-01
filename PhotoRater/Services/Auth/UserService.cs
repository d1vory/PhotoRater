using AutoMapper;
using HttpExceptions.Exceptions;
using PhotoRater.DTO.Auth;
using PhotoRater.Utils;

namespace PhotoRater.Services.Auth;

public class UserService: BaseService
{
    public UserService(BaseApplicationContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : base(db, mapper, webHostEnvironment, httpContextAccessor)
    {
    }

    public async Task<UserMeDTO> GetUserTheirInfo()
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var user = await _db.Users.FindAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User is not found");
        }

        var dto = _mapper.Map<UserMeDTO>(user);
        return dto;
    }
    
    
}