using Ganz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ganz.API.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(string UserName, string Password)
    {
        var user = await _authenticationService.LoginAsync(UserName, Password);

        return Ok(user);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(string UserName, string Password)
    {
        var user = await _authenticationService.RegisterAsync(UserName, Password);

        return Ok(user);
    }

    [NonAction]
    //[ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("GeneratenewToken")]
    public async Task<IActionResult> GeneratenewToken(string Token, string RefreshToken)
    {
        var result = await _authenticationService.GenerateNewToken(Token, RefreshToken);

        return Ok(result);
    }
}
