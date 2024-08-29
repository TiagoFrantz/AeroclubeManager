using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroclubeManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FlightSchoolSystemMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightSchools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 65535, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Document = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSchools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airplanes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    FlightSchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplanes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airplanes_FlightSchools_FlightSchoolId",
                        column: x => x.FlightSchoolId,
                        principalTable: "FlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ICAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FlightSchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airports_FlightSchools_FlightSchoolId",
                        column: x => x.FlightSchoolId,
                        principalTable: "FlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightSchoolLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FlightSchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSchoolLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightSchoolLinks_FlightSchools_FlightSchoolId",
                        column: x => x.FlightSchoolId,
                        principalTable: "FlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightSchoolReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolFlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 65535, nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    DateOfReview = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSchoolReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightSchoolReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightSchoolReviews_FlightSchools_SchoolFlightId",
                        column: x => x.SchoolFlightId,
                        principalTable: "FlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFlightSchools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FlightSchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFlightSchools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFlightSchools_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFlightSchools_FlightSchools_FlightSchoolId",
                        column: x => x.FlightSchoolId,
                        principalTable: "FlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AirplaneImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AirplaneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirplaneImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirplaneImages_Airplanes_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Airplanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AirplaneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FlightSchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airplanes_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Airplanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_FlightSchools_FlightSchoolId",
                        column: x => x.FlightSchoolId,
                        principalTable: "FlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_UserFlightSchools_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "UserFlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_UserFlightSchools_StudentId",
                        column: x => x.StudentId,
                        principalTable: "UserFlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFlightSchoolRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFlightSchoolRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFlightSchoolRoles_UserFlightSchools_UserId",
                        column: x => x.UserId,
                        principalTable: "UserFlightSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirplaneImages_AirplaneId",
                table: "AirplaneImages",
                column: "AirplaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Airplanes_FlightSchoolId",
                table: "Airplanes",
                column: "FlightSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_FlightSchoolId",
                table: "Airports",
                column: "FlightSchoolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirplaneId",
                table: "Flights",
                column: "AirplaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FlightSchoolId",
                table: "Flights",
                column: "FlightSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_InstructorId",
                table: "Flights",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_StudentId",
                table: "Flights",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSchoolLinks_FlightSchoolId",
                table: "FlightSchoolLinks",
                column: "FlightSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSchoolReviews_SchoolFlightId",
                table: "FlightSchoolReviews",
                column: "SchoolFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSchoolReviews_UserId",
                table: "FlightSchoolReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFlightSchoolRoles_UserId",
                table: "UserFlightSchoolRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFlightSchools_FlightSchoolId",
                table: "UserFlightSchools",
                column: "FlightSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFlightSchools_UserId",
                table: "UserFlightSchools",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirplaneImages");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "FlightSchoolLinks");

            migrationBuilder.DropTable(
                name: "FlightSchoolReviews");

            migrationBuilder.DropTable(
                name: "UserFlightSchoolRoles");

            migrationBuilder.DropTable(
                name: "Airplanes");

            migrationBuilder.DropTable(
                name: "UserFlightSchools");

            migrationBuilder.DropTable(
                name: "FlightSchools");
        }
    }
}
