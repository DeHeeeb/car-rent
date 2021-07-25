using Microsoft.EntityFrameworkCore.Migrations;

namespace car_rent_backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "City", "CustomerNr", "FirstName", "LastName", "Street", "Zip" },
                values: new object[,]
                {
                    { 1, "St. Gallen", "C123", "Hans", "Meier", "Turmstrasse 3", 9000 },
                    { 2, "Wil SG", "C463", "Jasmin", "Hugentobler", "Im Tobel 19", 9500 },
                    { 3, "Appenzell", "C932", "Reto", "Manser", "Bühlstrasse 18a", 9050 },
                    { 4, "St. Gallen", "C435", "Lukas", "Heeb", "Hauptstrasse 50", 9000 },
                    { 5, "Lausanne", "N773", "Hanna", "Muster", "Wurstweg 5", 1000 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
