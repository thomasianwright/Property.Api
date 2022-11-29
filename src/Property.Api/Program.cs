using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Property.Api.BusinessLogic.MappingProfiles;
using Property.Api.BusinessLogic.Services;
using Property.Api.Contracts.Repositories;
using Property.Api.Contracts.Services;
using Property.Api.Entities.Models;
using Property.Api.Infrastructure.Data;
using Property.Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(UserMapping).Assembly);
builder.Services.AddDbContext<ApiContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("ApiConnection"),
        optionsBuilder => { optionsBuilder.MigrationsAssembly(typeof(ApiContext).Assembly.FullName); });
});

builder.Services.AddSingleton<IHashids>(_ => new Hashids(builder.Configuration["Hashids:Salt"], 8));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IRentalAgreementRepository, RentalAgreementRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();