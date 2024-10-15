using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tourism_Management_System_API_Project_.Migrations
{
    /// <inheritdoc />
    public partial class InitialTableCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourPackage",
                columns: table => new
                {
                    TourID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    Location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPackage", x => x.TourID);
                });

            migrationBuilder.CreateTable(
                name: "UserManagement",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserManagement", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TourID = table.Column<int>(type: "int", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_TourID",
                        column: x => x.TourID,
                        principalTable: "TourPackage",
                        principalColumn: "TourID");
                    table.ForeignKey(
                        name: "FK_Bookings_UserId",
                        column: x => x.UserId,
                        principalTable: "UserManagement",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TourID = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    Comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Review_TourPackage_TourID",
                        column: x => x.TourID,
                        principalTable: "TourPackage",
                        principalColumn: "TourID");
                    table.ForeignKey(
                        name: "FK_Reviews_UserId",
                        column: x => x.UserId,
                        principalTable: "UserManagement",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TourID",
                table: "Booking",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserId",
                table: "Booking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_TourID",
                table: "Review",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserManagement_Email",
                table: "UserManagement",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserManagement_Username",
                table: "UserManagement",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "TourPackage");

            migrationBuilder.DropTable(
                name: "UserManagement");
        }
    }
}
