using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace car_rent_backend.Migrations
{
    public partial class Added_Reservation_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CarClass = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: true),
                    IsContract = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CarClass", "CustomerId", "EndDate", "IsContract", "ReservationNr", "StartDate", "Total" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2021, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "R123", new DateTime(2021, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 400.0 },
                    { 2, 0, 4, new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "R884", new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 800.0 },
                    { 3, 1, 2, new DateTime(2021, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "R241", new DateTime(2021, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 300.0 },
                    { 4, 2, 2, new DateTime(2021, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "R359", new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, 0, 5, new DateTime(2021, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "R305", new DateTime(2021, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, 2, 2, new DateTime(2021, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "R432", new DateTime(2021, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
