using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatrimonialApp.Migrations
{
    public partial class TransactionModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UPIID",
                table: "Transactions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UPIID",
                table: "Transactions");
        }
    }
}
