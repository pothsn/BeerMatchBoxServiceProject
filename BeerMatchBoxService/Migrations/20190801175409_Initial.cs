using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerMatchBoxService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BreweryDBBeer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BreweryDBBeerId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    BreweryName = table.Column<string>(nullable: true),
                    Abv = table.Column<double>(nullable: true),
                    Ibu = table.Column<double>(nullable: true),
                    GlasswareId = table.Column<int>(nullable: true),
                    StyleId = table.Column<int>(nullable: true),
                    StyleName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsOrganic = table.Column<string>(nullable: true),
                    IsRetired = table.Column<string>(nullable: true),
                    BreweryDBBreweryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreweryDBBeer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreweryDBBeer_BreweryDBBeer_BreweryDBBreweryId",
                        column: x => x.BreweryDBBreweryId,
                        principalTable: "BreweryDBBeer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BreweryDBBrewery",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BreweryDBBreweryId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NameShortDisplay = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Established = table.Column<int>(nullable: false),
                    IsOrganic = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: false),
                    Longitude = table.Column<decimal>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreweryDBBrewery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BreweryDBLabelHolder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BreweryDBBeerId = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Medium = table.Column<string>(nullable: true),
                    Large = table.Column<string>(nullable: true),
                    ContentAwareIcon = table.Column<string>(nullable: true),
                    ContentAwareMedium = table.Column<string>(nullable: true),
                    ContentAwareLarge = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreweryDBLabelHolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreweryDBLabelHolder_BreweryDBBeer_BreweryDBBeerId",
                        column: x => x.BreweryDBBeerId,
                        principalTable: "BreweryDBBeer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BreweryDBIconHolder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BreweryDBBreweryId = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Medium = table.Column<string>(nullable: true),
                    Large = table.Column<string>(nullable: true),
                    SquareMedium = table.Column<string>(nullable: true),
                    SquareLarge = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreweryDBIconHolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreweryDBIconHolder_BreweryDBBrewery_BreweryDBBreweryId",
                        column: x => x.BreweryDBBreweryId,
                        principalTable: "BreweryDBBrewery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBeer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BreweryDBBeerId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    BreweryName = table.Column<string>(nullable: true),
                    Abv = table.Column<double>(nullable: true),
                    Ibu = table.Column<double>(nullable: true),
                    GlasswareId = table.Column<int>(nullable: true),
                    StyleId = table.Column<int>(nullable: true),
                    StyleName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsOrganic = table.Column<string>(nullable: true),
                    IsRetired = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBeer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBeer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTaste",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    LikesBitter = table.Column<int>(nullable: false),
                    LikesFruity = table.Column<int>(nullable: false),
                    LikesSour = table.Column<int>(nullable: false),
                    LikesHoppy = table.Column<int>(nullable: false),
                    LikesMalty = table.Column<int>(nullable: false),
                    LikesChocolate = table.Column<int>(nullable: false),
                    LikesCoffee = table.Column<int>(nullable: false),
                    LikesSweet = table.Column<int>(nullable: false),
                    LikesStrong = table.Column<int>(nullable: false),
                    LikesSession = table.Column<int>(nullable: false),
                    LikesPale = table.Column<int>(nullable: false),
                    LikesMiddling = table.Column<int>(nullable: false),
                    LikesDark = table.Column<int>(nullable: false),
                    LikesBarrelAged = table.Column<int>(nullable: false),
                    LikesLager = table.Column<int>(nullable: false),
                    LikesAle = table.Column<int>(nullable: false),
                    LikesPaleAle = table.Column<int>(nullable: false),
                    LikesIPA = table.Column<int>(nullable: false),
                    LikesESB = table.Column<int>(nullable: false),
                    LikesStout = table.Column<int>(nullable: false),
                    LikesPorter = table.Column<int>(nullable: false),
                    LikesBrownAle = table.Column<int>(nullable: false),
                    LikesRedAle = table.Column<int>(nullable: false),
                    LikesWheat = table.Column<int>(nullable: false),
                    LikesSourBeer = table.Column<int>(nullable: false),
                    LikesSaison = table.Column<int>(nullable: false),
                    LikesBelgian = table.Column<int>(nullable: false),
                    LikesGerman = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaste_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BreweryDBBeerId = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BreweryName = table.Column<string>(nullable: true),
                    Abv = table.Column<double>(nullable: true),
                    Ibu = table.Column<double>(nullable: true),
                    GlasswareId = table.Column<int>(nullable: true),
                    StyleId = table.Column<string>(nullable: true),
                    StyleName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsOrganic = table.Column<string>(nullable: true),
                    IsRetired = table.Column<string>(nullable: true),
                    ImagesId = table.Column<int>(nullable: true),
                    BreweryDBBreweryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_BreweryDBLabelHolder_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "BreweryDBLabelHolder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BreweryDBBeer_BreweryDBBreweryId",
                table: "BreweryDBBeer",
                column: "BreweryDBBreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_BreweryDBIconHolder_BreweryDBBreweryId",
                table: "BreweryDBIconHolder",
                column: "BreweryDBBreweryId",
                unique: true,
                filter: "[BreweryDBBreweryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BreweryDBLabelHolder_BreweryDBBeerId",
                table: "BreweryDBLabelHolder",
                column: "BreweryDBBeerId",
                unique: true,
                filter: "[BreweryDBBeerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Match_ImagesId",
                table: "Match",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_UserId",
                table: "Match",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdentityUserId",
                table: "User",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBeer_UserId",
                table: "UserBeer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaste_UserId",
                table: "UserTaste",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BreweryDBIconHolder");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "UserBeer");

            migrationBuilder.DropTable(
                name: "UserTaste");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BreweryDBBrewery");

            migrationBuilder.DropTable(
                name: "BreweryDBLabelHolder");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BreweryDBBeer");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
