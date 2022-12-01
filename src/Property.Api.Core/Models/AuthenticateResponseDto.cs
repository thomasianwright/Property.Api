namespace Property.Api.Core.Models;

public class AuthenticateResponseDto
{
    public string Token { get; set; }
    public DateTime TokenExpiry { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }

    public AuthenticateResponseDto(string token, DateTime tokenExpiry, string refreshToken, DateTime refreshTokenExpiry)
    {
        Token = token;
        TokenExpiry = tokenExpiry;
        RefreshToken = refreshToken;
        RefreshTokenExpiry = refreshTokenExpiry;
    }
}