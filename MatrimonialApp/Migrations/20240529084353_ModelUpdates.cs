using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatrimonialApp.Migrations
{
    public partial class ModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matchs",
                columns: table => new
                {
                    MatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID1 = table.Column<int>(type: "int", nullable: false),
                    UserID2 = table.Column<int>(type: "int", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatchStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchs", x => x.MatchID);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherTongue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Interests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerExpectations = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileID);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    SubscriptionType = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Basic"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHashKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Matchs",
                columns: new[] { "MatchID", "MatchDate", "MatchStatus", "UserID1", "UserID2" },
                values: new object[] { 102, new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 102, 103 });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "ProfileID", "Caste", "Education", "Gender", "Height", "Income", "Interests", "MaritalStatus", "MotherTongue", "PartnerExpectations", "Religion", "UserID" },
                values: new object[] { 102, "Mali", "MCA", "Male", 102m, 950000m, "Sports", 1, "Hindi", "Nothing", "Hindu", 102 });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "SubscriptionID", "EndDate", "StartDate", "SubscriptionType", "UserID" },
                values: new object[] { 102, new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Normal", 102 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber", "ProfilePicture" },
                values: new object[,]
                {
                    { 104, "", new DateTime(2000, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amali@gmail.com", "Arvind", "Mali", "9876543321", "" },
                    { 105, "", new DateTime(2000, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amali1@gmail.com", "Arvind1", "Mali1", "9876543321", "" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matchs");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
