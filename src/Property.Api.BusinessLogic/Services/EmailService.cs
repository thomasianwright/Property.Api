using Property.Api.Contracts.Services;

namespace Property.Api.BusinessLogic.Services;

public class EmailService : IEmailService
{
    public EmailService()
    {
        
    }

    public Task SendNewUserEmail(string email, string name, string password)
    {
        throw new NotImplementedException();
    }
}