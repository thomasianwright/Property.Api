﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Property.Api.Infrastructure.Data;

#nullable disable

namespace Property.Api.Infrastructure.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Property.Api.Entities.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountUserOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUserOwnerId")
                        .IsUnique();

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("Property.Api.Entities.Models.AccountUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("AccountsId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "AccountId");

                    b.HasIndex("AccountId");

                    b.HasIndex("AccountsId");

                    b.ToTable("AccountUser");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LineOne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LineThree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LineTwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TradingAddressId")
                        .HasColumnType("int");

                    b.Property<string>("VatNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TradingAddressId")
                        .IsUnique();

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PropertyAddressId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyCompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("PropertyRentalAgreementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyAddressId")
                        .IsUnique();

                    b.HasIndex("PropertyCompanyId");

                    b.ToTable("Property", (string)null);
                });

            modelBuilder.Entity("Property.Api.Entities.Models.RentalAgreement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Files")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RentalAgreementAccountId")
                        .HasColumnType("int");

                    b.Property<int>("RentalAgreementCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("RentalAgreementPropertyId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RentalAgreementAccountId");

                    b.HasIndex("RentalAgreementCompanyId");

                    b.HasIndex("RentalAgreementPropertyId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("RentalAgreement", (string)null);
                });

            modelBuilder.Entity("Property.Api.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastLoginIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserAddressId")
                        .HasColumnType("int");

                    b.Property<int?>("UserCompanyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Account", b =>
                {
                    b.HasOne("Property.Api.Entities.Models.User", "AccountOwner")
                        .WithOne()
                        .HasForeignKey("Property.Api.Entities.Models.Account", "AccountUserOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountOwner");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.AccountUser", b =>
                {
                    b.HasOne("Property.Api.Entities.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Property.Api.Entities.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Property.Api.Entities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Company", b =>
                {
                    b.HasOne("Property.Api.Entities.Models.Address", "TradingAddress")
                        .WithOne()
                        .HasForeignKey("Property.Api.Entities.Models.Company", "TradingAddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TradingAddress");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Property", b =>
                {
                    b.HasOne("Property.Api.Entities.Models.Address", "PropertyAddress")
                        .WithOne()
                        .HasForeignKey("Property.Api.Entities.Models.Property", "PropertyAddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Property.Api.Entities.Models.Company", "Company")
                        .WithMany("Properties")
                        .HasForeignKey("PropertyCompanyId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("PropertyAddress");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.RentalAgreement", b =>
                {
                    b.HasOne("Property.Api.Entities.Models.Account", "Account")
                        .WithMany("RentalAgreements")
                        .HasForeignKey("RentalAgreementAccountId");

                    b.HasOne("Property.Api.Entities.Models.Company", "Company")
                        .WithMany("RentalAgreements")
                        .HasForeignKey("RentalAgreementCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Property.Api.Entities.Models.Property", "Property")
                        .WithOne("RentalAgreement")
                        .HasForeignKey("Property.Api.Entities.Models.RentalAgreement", "RentalAgreementPropertyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Property.Api.Entities.Models.User", null)
                        .WithMany("RentalAgreements")
                        .HasForeignKey("UserId");

                    b.Navigation("Account");

                    b.Navigation("Company");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.User", b =>
                {
                    b.HasOne("Property.Api.Entities.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Property.Api.Entities.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.Navigation("Address");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Account", b =>
                {
                    b.Navigation("RentalAgreements");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Company", b =>
                {
                    b.Navigation("Properties");

                    b.Navigation("RentalAgreements");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.Property", b =>
                {
                    b.Navigation("RentalAgreement");
                });

            modelBuilder.Entity("Property.Api.Entities.Models.User", b =>
                {
                    b.Navigation("RentalAgreements");
                });
#pragma warning restore 612, 618
        }
    }
}
