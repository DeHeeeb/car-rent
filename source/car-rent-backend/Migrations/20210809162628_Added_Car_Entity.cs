using Microsoft.EntityFrameworkCore.Migrations;

namespace car_rent_backend.Migrations
{
    public partial class Added_Car_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "Id", "Brand", "CarNr", "Class", "Type" },
                values: new object[,]
                {
                    { 1, "Peugeot", "1001", "Low", "Limousine" },
                    { 2, "Citroen", "1015", "Medium", "Minivan" },
                    { 3, "Chevrolet", "5075", "High", "Convertible" },
                    { 4, "VW", "1043", "Medium", "Limousine" },
                    { 5, "BMW", "9311", "Medium", "Convertible" },
                    { 6, "Suzuki", "1353", "High", "Minivan" },
                    { 7, "Tesla", "3197", "High", "Limousine" },
                    { 8, "VW", "4220", "Medium", "Minivan" },
                    { 9, "VW", "8314", "Low", "Limousine" },
                    { 10, "VW", "4750", "Low", "Limousine" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");
        }
    }
}
