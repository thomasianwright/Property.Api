namespace Property.Api.Contracts.Services;

public interface IEmailService
{
    Task SendNewUserEmail(string email, string name, string password);
}