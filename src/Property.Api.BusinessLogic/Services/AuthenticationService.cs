using System.Security.Authentication;
using System.Text;
using AutoMapper;
using Property.Api.Contracts.Repositories;
using Property.Api.Contracts.Services;
using Property.Api.Core.Models;
using Property.Api.Entities.Models;

namespace Property.Api.BusinessLogic.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public AuthenticationService(IUserRepository userRepository, IMapper mapper, IEmailService emailService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _emailService = emailService;
    }
    
    public async Task<User> Authenticate(LoginDto loginDto)
    {
        var user = await _userRepository.GetAsync(loginDto.Email);
        if (user == null)
            throw new NullReferenceException("User not found");

        if (!ComparePassword(loginDto.Password, user.Password))
            throw new AuthenticationException("Password does not match");

        return user;
    }

    public async Task<User?> Authenticate(string refreshToken)
    {
        var hashedRefreshToken = HashPassword(refreshToken);
        
        var user = await _userRepository.GetByRefreshTokenAsync(hashedRefreshToken);

        return user;
    }

    public async Task AddRefreshToken(User user, string refreshToken, string ip, DateTime expiry)
    {
        user.RefreshToken = HashPassword(refreshToken);
        user.LastLoginIp = ip;
        user.LastLoginDate = DateTime.UtcNow;
        user.RefreshTokenExpiryTime = expiry;
        
        await _userRepository.UpdateAsync(user);
    }
    
    public async Task<User?> Register(CreateUserDto registerDto)
    {
        var user = await _userRepository.GetAsync(registerDto.Email);
        if (user != null)
            throw new AuthenticationException("User already exists");

        var newUser = new User();
        
        if (string.IsNullOrWhiteSpace(registerDto.Password))
        {
            var password = CreatePassword(10);
            var name = $"{registerDto.FirstName} {registerDto.LastName}";
            registerDto.Password = HashPassword(password);
            
            await _emailService.SendNewUserEmail(registerDto.Email, name, password);
        };
        
        _mapper.Map(registerDto, newUser);

        
        newUser.Password = HashPassword(registerDto.Password);

        await _userRepository.AddAsync(newUser);
        return newUser;
    }

    private static bool ComparePassword(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(password, hash);
    
    private static string HashPassword(string password) 
        => BCrypt.Net.BCrypt.HashPassword(password);
    
    private static string CreatePassword(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!$%&/()=?*#";
        var res = new StringBuilder();
        var rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
}