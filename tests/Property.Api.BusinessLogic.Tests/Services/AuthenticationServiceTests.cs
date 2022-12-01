using AutoMapper;
using FluentAssertions;
using Moq;
using Property.Api.BusinessLogic.MappingProfiles;
using Property.Api.BusinessLogic.Services;
using Property.Api.Contracts.Repositories;
using Property.Api.Contracts.Services;
using Property.Api.Core.Models;

namespace Property.Api.BusinessLogic.Tests.Services;

public class AuthenticationServiceTests
{
    public AuthenticationServiceTests()
    {
    }

    [Fact]
    public async Task Assert_Signup_Of_User_Without_Password_Sends_Password_Via_EmailService()
    {
        var emailService = new Mock<IEmailService>();
        var userService = new Mock<IUserRepository>();
        var mapper = new Mapper(new MapperConfiguration((config =>
        {
            config.AddProfiles(new Profile[]
            {
                new UserMapping(), new AddressMapping(), new AccountMapping(), new CompanyMapping(),
                new PropertyMapping(), new AccountMapping(), new RentalAgreementMapping()
            });
        })));

        var authenticationService = new AuthenticationService(userService.Object, mapper, emailService.Object);

        emailService.Setup(x => x.SendNewUserEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var registerDto = new CreateUserDto
        {
            FirstName = "Thomas",
            LastName = "Wright",
            Email = "thomas.wright@example.com",
            Address = new CreateAddressDto
            {
                LineOne = "123 Fake Street",
                LineTwo = "Fake Town",
                Postcode = "FAKE1",
                Country = "Fake Country",
                County = "Fake County"
            },
            Password = null
        };
        
        var user = await authenticationService.Register(registerDto);

        emailService.Verify(x => x.SendNewUserEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);

        user!.FirstName.Should().BeSameAs(registerDto.FirstName);
        user.LastName.Should().BeSameAs(registerDto.LastName);
        user.Email.Should().BeSameAs(registerDto.Email);
        user.Address.LineOne.Should().BeSameAs(registerDto.Address.LineOne);
        user.Address.LineTwo.Should().BeSameAs(registerDto.Address.LineTwo);
        user.Address.Postcode.Should().BeSameAs(registerDto.Address.Postcode);
        user.Address.Country.Should().BeSameAs(registerDto.Address.Country);
        user.Address.County.Should().BeSameAs(registerDto.Address.County);
    }
}