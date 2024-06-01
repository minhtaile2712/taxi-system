using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaxiSystem.Migrations
{
    /// <inheritdoc />
    public partial class Update_RelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BookingDriver",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookingId = table.Column<long>(type: "bigint", nullable: false),
                    DriverId = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    PositionInQueue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDriver_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDriver_BookingId",
                table: "BookingDriver",
                column: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDriver");

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
    }
}
