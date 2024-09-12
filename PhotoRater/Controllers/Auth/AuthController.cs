using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
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
        if (!ModelState.IsValid) return BadRequest(ModelState.StringifyModelErrors());
        var isCreated = await _service.RegisterUser(registerUserDto, ModelState);
        if (!isCreated)
        {
            return BadRequest(ModelState.StringifyModelErrors());
        }

        return Ok("User is registered!");

    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.StringifyModelErrors());
        var accessToken = await _service.Login(loginDto, ModelState);
        var refreshToken = await _service.GetRefreshToken(loginDto.Username);
        return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken), RefreshToken = refreshToken});
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult> Refresh([FromBody] RefreshTokenDTO dto)
    {
        var accessToken = await _service.RefreshLogin(dto);
        //var refreshToken = await _service.GetRefreshToken();
        return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken)});
    }

    [HttpGet]
    [Authorize]
    [Route("kek")]
    public ActionResult Kek()
    {
        return Ok("EVERYTHING IS GOOD");
    }

}