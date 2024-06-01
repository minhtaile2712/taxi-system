using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiSystem.Migrations
{
    /// <inheritdoc />
    public partial class Add_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookingDriver_DriverId",
                table: "BookingDriver",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDriver_Drivers_DriverId",
                table: "BookingDriver",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDriver_Drivers_DriverId",
                table: "BookingDriver");

            migrationBuilder.DropIndex(
                name: "IX_BookingDriver_DriverId",
                table: "BookingDriver");
        }
    }
}
