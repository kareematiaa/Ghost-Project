using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditAdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAddress_NearestLandMark",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerAddress_PostalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerAddress_NearestLandMark",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerAddress_PostalCode",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress_NearestLandMark",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress_PostalCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress_NearestLandMark",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress_PostalCode",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
