﻿// <auto-generated />
using System;
using BeerMatchBoxService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeerMatchBoxService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeerMatchBoxService.Models.BreweryDBBeer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Abv");

                    b.Property<string>("BreweryDBBeerId");

                    b.Property<string>("BreweryDBBreweryId");

                    b.Property<string>("BreweryName");

                    b.Property<string>("Description");

                    b.Property<int?>("GlasswareId");

                    b.Property<double?>("Ibu");

                    b.Property<string>("IsOrganic");

                    b.Property<string>("IsRetired");

                    b.Property<string>("Name");

                    b.Property<int?>("StyleId");

                    b.Property<string>("StyleName");

                    b.HasKey("Id");

                    b.HasIndex("BreweryDBBreweryId");

                    b.ToTable("BreweryDBBeer");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.BreweryDBBrewery", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BreweryDBBreweryId");

                    b.Property<string>("City");

                    b.Property<string>("Description");

                    b.Property<int>("Established");

                    b.Property<string>("IsOrganic");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("NameShortDisplay");

                    b.Property<string>("State");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.ToTable("BreweryDBBrewery");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.BreweryDBIconHolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BreweryDBBreweryId");

                    b.Property<string>("Icon");

                    b.Property<string>("Large");

                    b.Property<string>("Medium");

                    b.Property<string>("SquareLarge");

                    b.Property<string>("SquareMedium");

                    b.HasKey("Id");

                    b.HasIndex("BreweryDBBreweryId")
                        .IsUnique()
                        .HasFilter("[BreweryDBBreweryId] IS NOT NULL");

                    b.ToTable("BreweryDBIconHolder");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.BreweryDBLabelHolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BreweryDBBeerId");

                    b.Property<string>("ContentAwareIcon");

                    b.Property<string>("ContentAwareLarge");

                    b.Property<string>("ContentAwareMedium");

                    b.Property<string>("Icon");

                    b.Property<string>("Large");

                    b.Property<string>("Medium");

                    b.HasKey("Id");

                    b.HasIndex("BreweryDBBeerId")
                        .IsUnique()
                        .HasFilter("[BreweryDBBeerId] IS NOT NULL");

                    b.ToTable("BreweryDBLabelHolder");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.Match", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Abv");

                    b.Property<string>("BreweryDBBeerId");

                    b.Property<string>("BreweryDBBreweryId");

                    b.Property<string>("BreweryName");

                    b.Property<string>("Description");

                    b.Property<int?>("GlasswareId");

                    b.Property<double?>("Ibu");

                    b.Property<int?>("ImagesId");

                    b.Property<string>("IsOrganic");

                    b.Property<string>("IsRetired");

                    b.Property<string>("Name");

                    b.Property<string>("StyleId");

                    b.Property<string>("StyleName");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ImagesId");

                    b.HasIndex("UserId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("IdentityUserId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.Property<string>("Zipcode");

                    b.HasKey("Id");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.UserBeer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Abv");

                    b.Property<string>("BreweryDBBeerId");

                    b.Property<string>("BreweryName");

                    b.Property<string>("Description");

                    b.Property<int?>("GlasswareId");

                    b.Property<double?>("Ibu");

                    b.Property<string>("IsOrganic");

                    b.Property<string>("IsRetired");

                    b.Property<string>("Name");

                    b.Property<int?>("StyleId");

                    b.Property<string>("StyleName");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserBeer");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.UserTaste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LikesAle");

                    b.Property<int>("LikesBarrelAged");

                    b.Property<int>("LikesBelgian");

                    b.Property<int>("LikesBitter");

                    b.Property<int>("LikesBrownAle");

                    b.Property<int>("LikesChocolate");

                    b.Property<int>("LikesCoffee");

                    b.Property<int>("LikesDark");

                    b.Property<int>("LikesESB");

                    b.Property<int>("LikesFruity");

                    b.Property<int>("LikesGerman");

                    b.Property<int>("LikesHoppy");

                    b.Property<int>("LikesIPA");

                    b.Property<int>("LikesLager");

                    b.Property<int>("LikesMalty");

                    b.Property<int>("LikesMiddling");

                    b.Property<int>("LikesPale");

                    b.Property<int>("LikesPaleAle");

                    b.Property<int>("LikesPorter");

                    b.Property<int>("LikesRedAle");

                    b.Property<int>("LikesSaison");

                    b.Property<int>("LikesSession");

                    b.Property<int>("LikesSour");

                    b.Property<int>("LikesSourBeer");

                    b.Property<int>("LikesStout");

                    b.Property<int>("LikesStrong");

                    b.Property<int>("LikesSweet");

                    b.Property<int>("LikesWheat");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTaste");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.BreweryDBBeer", b =>
                {
                    b.HasOne("BeerMatchBoxService.Models.BreweryDBBeer", "BreweryDBBrewery")
                        .WithMany()
                        .HasForeignKey("BreweryDBBreweryId");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.BreweryDBIconHolder", b =>
                {
                    b.HasOne("BeerMatchBoxService.Models.BreweryDBBrewery", "BreweryDBBrewery")
                        .WithOne("Images")
                        .HasForeignKey("BeerMatchBoxService.Models.BreweryDBIconHolder", "BreweryDBBreweryId");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.BreweryDBLabelHolder", b =>
                {
                    b.HasOne("BeerMatchBoxService.Models.BreweryDBBeer", "BreweryDBBeer")
                        .WithOne("Images")
                        .HasForeignKey("BeerMatchBoxService.Models.BreweryDBLabelHolder", "BreweryDBBeerId");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.Match", b =>
                {
                    b.HasOne("BeerMatchBoxService.Models.BreweryDBLabelHolder", "Images")
                        .WithMany()
                        .HasForeignKey("ImagesId");

                    b.HasOne("BeerMatchBoxService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.User", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId");
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.UserBeer", b =>
                {
                    b.HasOne("BeerMatchBoxService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeerMatchBoxService.Models.UserTaste", b =>
                {
                    b.HasOne("BeerMatchBoxService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
