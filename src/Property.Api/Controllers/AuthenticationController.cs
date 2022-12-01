using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Property.Api.Contracts.Services;
using Property.Api.Core.Helpers;
using Property.Api.Core.Models;

namespace Property.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthenticationController(IAuthenticationService authenticationService, ITokenService tokenService, IMapper mapper)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
        _mapper = mapper;
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

            return Ok(new AuthenticateResponseDto(token, securityToken.ValidTo, refreshToken, refreshTokenExpiry));
        }
        catch (NullReferenceException e)
        {
            return BadRequest(new ErrorResponseDto(e.Message, ErrorCodes.NullProperty));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponseDto("An unknown error occurred", ErrorCodes.UnknownError));
        }
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken(RegenerateTokenDto regenerateTokenDto)
    {
        try
        {
            var user = await _authenticationService.Authenticate(regenerateTokenDto.Token);
            
            var expiry = DateTime.Now.AddMinutes(2);
            
            var (token, securityToken) = _tokenService.GenerateToken(user, expiry);
            
            return Ok(new RegenerateTokenResponseDto(token, expiry));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponseDto("An unknown error has occured", ErrorCodes.UnknownError));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserDto userDto)
    {
        try
        {
            var user = await _authenticationService.Register(userDto);

            var newUser = _mapper.Map<UserDto>(user);
            
            return Ok(newUser);
        }
        catch(Exception e)
        {
            return BadRequest(new ErrorResponseDto("An unknown error has occured", ErrorCodes.UnknownError));
        }
    }
}