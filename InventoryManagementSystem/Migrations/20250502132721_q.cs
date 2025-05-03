using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class q : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SourceWarehouseId",
                table: "InventoryTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetWarehouseId",
                table: "InventoryTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WareHouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductWareHouseStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WareHouseId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWareHouseStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWareHouseStocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWareHouseStocks_WareHouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "WareHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_SourceWarehouseId",
                table: "InventoryTransactions",
                column: "SourceWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_TargetWarehouseId",
                table: "InventoryTransactions",
                column: "TargetWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWareHouseStocks_ProductId",
                table: "ProductWareHouseStocks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWareHouseStocks_WareHouseId",
                table: "ProductWareHouseStocks",
                column: "WareHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_WareHouses_SourceWarehouseId",
                table: "InventoryTransactions",
                column: "SourceWarehouseId",
                principalTable: "WareHouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_WareHouses_TargetWarehouseId",
                table: "InventoryTransactions",
                column: "TargetWarehouseId",
                principalTable: "WareHouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_WareHouses_SourceWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_WareHouses_TargetWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropTable(
                name: "ProductWareHouseStocks");

            migrationBuilder.DropTable(
                name: "WareHouses");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransactions_SourceWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransactions_TargetWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "SourceWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "TargetWarehouseId",
                table: "InventoryTransactions");
        }
    }
}
