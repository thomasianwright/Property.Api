using Property.Api.Core.Models;
using Property.Api.Entities.Models;

namespace Property.Api.Contracts.Services;

public interface IAuthenticationService
{
    Task<User> Authenticate(LoginDto loginDto);
    Task<User?> Authenticate(string refreshToken);
    Task<User?> Register(CreateUserDto registerDto);
    Task AddRefreshToken(User user, string refreshToken, string ip, DateTime expiry);
}