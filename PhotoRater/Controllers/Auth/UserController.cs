using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoRater.DTO.Auth;
using PhotoRater.Services.Auth;

namespace PhotoRater.Controllers.Auth;

[Route("user")]
public class UserController: ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    [Route("me")]
    public async Task<ActionResult<UserMeDTO>> GetNextPhotoForRate()
    {
        var dto = await _service.GetUserTheirInfo();
        return Ok(dto);
    }
}