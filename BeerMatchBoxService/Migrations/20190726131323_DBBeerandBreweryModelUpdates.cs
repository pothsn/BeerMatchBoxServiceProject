using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerMatchBoxService.Migrations
{
    public partial class DBBeerandBreweryModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreweryDBBeer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BreweryDBBeerId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    BreweryName = table.Column<string>(nullable: true),
                    Abv = table.Column<double>(nullable: false),
                    GlasswareId = table.Column<int>(nullable: false),
                    StyleId = table.Column<int>(nullable: false),
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
                    IsOrganic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreweryDBBrewery", x => x.Id);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreweryDBIconHolder");

            migrationBuilder.DropTable(
                name: "BreweryDBLabelHolder");

            migrationBuilder.DropTable(
                name: "BreweryDBBrewery");

            migrationBuilder.DropTable(
                name: "BreweryDBBeer");
        }
    }
}
