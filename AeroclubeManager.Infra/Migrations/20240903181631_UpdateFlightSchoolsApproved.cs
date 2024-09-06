using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroclubeManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFlightSchoolsApproved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "FlightSchools",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "FlightSchools");
        }
    }
}
