using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Property.Api.Contracts.Services;
using Property.Api.Core.Models;

namespace Property.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenService _tokenService;

    public AuthenticationController(IAuthenticationService authenticationService, ITokenService tokenService)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        try
        {
            Guard.Against.Null(request);
            Guard.Against.NullOrWhiteSpace(request.Email);
            Guard.Against.NullOrWhiteSpace(request.Password);
            
            var user = await _authenticationService.Authenticate(request);
            var expiry = DateTime.Now.AddMinutes(2);
            var refreshTokenExpiry = DateTime.Now.AddDays(7);

            var (token, securityToken) = _tokenService.GenerateToken(user, expiry);

            var refreshToken = _tokenService.GenerateRefreshToken();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _authenticationService.AddRefreshToken(user, refreshToken, ip, refreshTokenExpiry);

            return Ok(new
            {
                token,
                refreshToken,
                expiry,
                refreshTokenExpiry
            });
        }
        catch (NullReferenceException e)
        {
            return BadRequest(new
            {
                message = e.Message
            });
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                message = "Something went wrong, please try again!"
            });
        }
    }
}