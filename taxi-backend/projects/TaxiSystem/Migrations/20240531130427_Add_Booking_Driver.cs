using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiSystem.Migrations
{
    /// <inheritdoc />
    public partial class Add_Booking_Driver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BookingId",
                table: "Drivers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_BookingId",
                table: "Drivers",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Bookings_BookingId",
                table: "Drivers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Bookings_BookingId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_BookingId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Drivers");
        }
    }
}
