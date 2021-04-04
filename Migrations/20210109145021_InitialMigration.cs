using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent_a_car.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    InsuranceCost = table.Column<decimal>(nullable: false),
                    MaintenanceCost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(nullable: false),
                    ExpDriverLicense = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(nullable: false),
                    StorageTime = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garages_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: true),
                    UserRole = table.Column<int>(nullable: false),
                    DriverInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_DriverInfos_DriverInfoId",
                        column: x => x.DriverInfoId,
                        principalTable: "DriverInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarUsage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Range = table.Column<int>(nullable: false),
                    GarageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarUsage_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarUsers_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CreditCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Limit = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CreditCardId);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarUsage_GarageId",
                table: "CarUsage",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_CarUsers_CarId",
                table: "CarUsers",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarUsers_UserId",
                table: "CarUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UserId",
                table: "CreditCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Garages_CarId",
                table: "Garages",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DriverInfoId",
                table: "Users",
                column: "DriverInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarUsage");

            migrationBuilder.DropTable(
                name: "CarUsers");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Garages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "DriverInfos");
        }
    }
}
