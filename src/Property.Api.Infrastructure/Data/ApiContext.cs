using System.Text.Json;
using PropertyModel = Property.Api.Entities.Models.Property;

namespace Property.Api.Infrastructure.Data;

public class ApiContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<PropertyModel?> Properties { get; set; }
    public DbSet<RentalAgreement?> RentalAgreements { get; set; }
    public DbSet<User> Users { get; set; }

    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        var jsonOptions = new JsonSerializerOptions();

        mb.Entity<Account>().ToTable("Account");
        mb.Entity<Address>().ToTable("Address");
        mb.Entity<Company>().ToTable("Company");
        mb.Entity<PropertyModel>().ToTable("Property");
        mb.Entity<RentalAgreement>().ToTable("RentalAgreement");
        mb.Entity<User>().ToTable("User");

        mb.Entity<Account>()
            .HasMany(x => x.Users)
            .WithMany(x => x.Accounts);

        mb.Entity<Account>()
            .HasMany(x => x.RentalAgreements)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.RentalAgreementAccountId);

        mb.Entity<Company>()
            .HasOne(x => x.TradingAddress)
            .WithOne()
            .HasForeignKey<Company>(x => x.TradingAddressId);

        mb.Entity<PropertyModel>()
            .HasOne(x => x.PropertyAddress)
            .WithOne()
            .HasForeignKey<PropertyModel>(x => x.PropertyAddressId);

        mb.Entity<PropertyModel>()
            .HasOne(x => x.Company)
            .WithMany(x => x.Properties)
            .HasForeignKey(x => x.PropertyCompanyId);

        mb.Entity<RentalAgreement>()
            .HasOne(x => x.Account)
            .WithMany(x => x.RentalAgreements)
            .HasForeignKey(x => x.RentalAgreementAccountId);

        mb.Entity<RentalAgreement>()
            .HasOne(x => x.Company)
            .WithMany(x => x.RentalAgreements)
            .HasForeignKey(x => x.RentalAgreementCompanyId);

        mb.Entity<RentalAgreement>()
            .Property(x => x.Files)
            .HasConversion(x => JsonSerializer.Serialize(x, jsonOptions),
                x => JsonSerializer.Deserialize<List<Guid>>(x, jsonOptions) ?? new List<Guid>());
        
        mb.Entity<Account>()
            .HasOne(x=> x.AccountOwner)
            .WithOne()
            .HasForeignKey<Account>(x => x.AccountUserOwnerId);
        
        mb.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();
        
        base.OnModelCreating(mb);
    }
}