using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageCategory = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Total = table.Column<double>(type: "double", nullable: true),
                    IsFinalised = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsCancelled = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "ImageCategory" },
                values: new object[,]
                {
                    { 1, "Baked", "Baked" },
                    { 2, "Beverage", "Beverage" },
                    { 3, "ColdMeat", "ColdMeat" },
                    { 4, "Dairy", "Dairy" },
                    { 5, "FreshFoods", "FreshFoods" },
                    { 6, "Frozen", "Frozen" },
                    { 7, "Pantry", "Pantry" },
                    { 8, "SelfCare", "SelfCare" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "$2a$11$sKyvVCLFoOAlorqabc8ExOw4otyDlqxgQK7WXMYF.Dl8.N4CJxgo6", "User1", "Test" },
                    { 2, "$2a$11$5gflcb8NKkpE5v9ud9Rx.epIaHliPKIdRG3fxSIeRTlhc6MyVSEOu", "User2", "Test1" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "ImageName", "ItemName", "Price", "Unit" },
                values: new object[,]
                {
                    { 1, 1, "Baked1", "Arnott's Scotch Finger", 2.8500000000000001, "250 g" },
                    { 2, 1, "Baked2", "Arnott's TimTam Original Chocolate", 3.6499999999999999, "200 g" },
                    { 3, 1, "Baked3", "Cheddar Shapes", 2.0, "175 g" },
                    { 4, 1, "Baked4", "Helga's Wholemeal Bread", 3.7000000000000002, "1 pk" },
                    { 5, 1, "Baked5", "Weet Bix Saniatatium", 5.3300000000000001, "750 g" },
                    { 6, 2, "Beverage1", "Berri Orange Juice", 6.0, "2 Litre" },
                    { 7, 2, "Beverage2", "Coca Cola", 2.8500000000000001, "2 Litre" },
                    { 8, 3, "ColdMeat1", "Macro Free Range Chicken", 7.5, "1 kg" },
                    { 9, 3, "ColdMeat2", "Primo English Ham", 3.0, "100 g" },
                    { 10, 3, "ColdMeat3", "Primo Short Cut Rindless Bacon", 5.0, "175 g" },
                    { 11, 4, "Dairy1", "A2 Light Milk", 2.8999999999999999, "1 Litre" },
                    { 12, 4, "Dairy2", "Babybel Mini", 4.2000000000000002, "100 g" },
                    { 13, 4, "Dairy3", "Bega Farmers Tasty", 4.0, "250 g" },
                    { 14, 4, "Dairy4", "Chobani Strawbery Yoghurt", 1.5, "140 g" },
                    { 15, 4, "Dairy5", "Lurpak Salted Blend", 5.0, "250 g" },
                    { 16, 4, "Dairy6", "Manning Valley Eggs", 3.6000000000000001, "6-pk" },
                    { 17, 4, "Dairy7", "Philadelphia Original Cream Cheese", 4.2999999999999998, "250 g" },
                    { 18, 4, "Dairy8", "South Cape Greek Feta", 5.0, "200 g" },
                    { 19, 5, "FreshFoods1", "Carrots", 2.0, "1 kg" },
                    { 20, 5, "FreshFoods2", "Cucumber", 1.8999999999999999, "1 whole" },
                    { 21, 5, "FreshFoods3", "Fresh Tomato", 5.9000000000000004, "500 g" },
                    { 22, 5, "FreshFoods4", "Granny Smith's Apples", 5.5, "1 kg" },
                    { 23, 5, "FreshFoods5", "Iceberg Lettuce", 2.5, " 1 whole" },
                    { 24, 5, "FreshFoods6", "Red Onion", 3.5, "1 kg" },
                    { 25, 5, "FreshFoods7", "Red Potato Washed", 4.0, "1 kg" },
                    { 26, 5, "FreshFoods8", "Red Tipped Bananas", 4.9000000000000004, "1 kg" },
                    { 27, 5, "FreshFoods9", "Watermelon", 6.5999999999999996, "1 whole" },
                    { 28, 6, "Frozen1", "Birds Eye Fish Fingers", 4.5, "375 g" },
                    { 29, 7, "Pantry01", "Cadburry Dairy Milk", 5.0, "200 g" },
                    { 30, 7, "Pantry02", "Capilano Squeezable Honey", 7.3499999999999996, "500 g" },
                    { 31, 7, "Pantry03", "Cobram Extra Virgin Olive Oil", 8.0, "375 ml" },
                    { 32, 7, "Pantry04", "Golden Circle Pineapple Pieces in natural juice", 3.25, "440 g" },
                    { 33, 7, "Pantry05", "Heinz Tomato Soup", 2.5, "535 g" },
                    { 34, 7, "Pantry06", "John West Tuna can", 1.5, "95 g" },
                    { 35, 7, "Pantry07", "Lindt Excellence 70% cocoa Block", 4.25, "100 g" },
                    { 36, 7, "Pantry08", "Milo Chocolate Malt", 4.0, "200 g" },
                    { 37, 7, "Pantry09", "Moccana Classic Instant Medium Roast", 6.0, "100 g" },
                    { 38, 7, "Pantry10", "Nutella Jar", 4.0, "400 g" },
                    { 39, 7, "Pantry11", "Sacla Pasta Tomato Basil Sauce", 4.5, "420 g" },
                    { 40, 7, "Pantry12", "San Remo Linguine Pasta No. 1", 1.95, "500 g" },
                    { 41, 7, "Pantry13", "Smith's Original Share Pack Crisps", 3.29, "170 g" },
                    { 42, 7, "Pantry14", "Vegemite", 6.0, "380 g" },
                    { 43, 8, "SelfCare1", "Colgate Total Toothpaste Original", 3.5, "110 g" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "Date", "IsCancelled", "IsFinalised", "Total", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 15, 18, 29, 54, 733, DateTimeKind.Local).AddTicks(4680), false, false, 25.0, 1 });

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "Id", "ProductsId", "Quantity", "ShoppingCartId" },
                values: new object[] { 1, 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductsId",
                table: "ShoppingCartItems",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
