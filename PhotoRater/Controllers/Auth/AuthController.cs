using Microsoft.AspNetCore.Mvc;
using PhotoRater.DTO.Auth;
using PhotoRater.Services.Auth;
using PhotoRater.Utils;

namespace PhotoRater.Controllers.Auth;

public class AuthController: ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("registration")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
    {
        if (ModelState.IsValid)
        {
            var isCreated = await _service.RegisterUser(registerUserDto, ModelState);
            if (!isCreated)
            {
                return BadRequest(ModelState.StringifyModelErrors());
            }

            return Ok("User is registered!");
        }
        
        return BadRequest(ModelState.StringifyModelErrors());
    }
}