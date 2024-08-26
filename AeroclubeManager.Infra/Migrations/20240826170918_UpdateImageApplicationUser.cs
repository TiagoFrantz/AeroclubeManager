using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroclubeManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlDeletePerfilImage",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlDeletePerfilImage",
                table: "AspNetUsers");
        }
    }
}
