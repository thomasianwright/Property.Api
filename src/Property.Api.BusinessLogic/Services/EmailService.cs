using Property.Api.Contracts.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Property.Api.BusinessLogic.Services;

public class EmailService : IEmailService
{
    public Task SendNewUserEmail(string email, string name, string? password)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetEmail(string email, string name, string passwordResetToken, string passwordResetId)
    {
        throw new NotImplementedException();
    }
}