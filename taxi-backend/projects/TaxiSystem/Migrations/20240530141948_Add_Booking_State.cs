using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiSystem.Migrations
{
    /// <inheritdoc />
    public partial class Add_Booking_State : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Bookings");
        }
    }
}
