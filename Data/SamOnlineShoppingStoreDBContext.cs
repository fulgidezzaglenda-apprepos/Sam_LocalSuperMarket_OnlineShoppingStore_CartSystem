using Microsoft.EntityFrameworkCore;
using Sam_LocalSuperMarket_OnlineShoppingStore1.Models;

namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Data
{
    public class SamOnlineShoppingStoreDBContext : DbContext
    {
        public SamOnlineShoppingStoreDBContext(DbContextOptions dbOptions) : base(dbOptions)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().HasData(
                new Products { Id = 01, ImageName = "Baked1", CategoryId = 01, Unit = "250 g", Price = 2.85, ItemName = "Arnott's Scotch Finger" },
                new Products { Id = 02, ImageName = "Baked2", CategoryId = 01, Unit = "200 g", Price = 3.65, ItemName = "Arnott's TimTam Original Chocolate" },
                new Products { Id = 03, ImageName = "Baked3", CategoryId = 01, Unit = "175 g", Price = 2.00, ItemName = "Cheddar Shapes" },
                new Products { Id = 04, ImageName = "Baked4", CategoryId = 01, Unit = "1 pk",  Price = 3.70, ItemName = "Helga's Wholemeal Bread" },
                new Products { Id = 05, ImageName = "Baked5", CategoryId = 01, Unit = "750 g", Price = 5.33, ItemName = "Weet Bix Saniatatium" },
                new Products { Id = 06, ImageName = "Beverage1", CategoryId = 02, Unit = "2 Litre", Price = 6.00, ItemName = "Berri Orange Juice" },
                new Products { Id = 07, ImageName = "Beverage2", CategoryId = 02, Unit = "2 Litre", Price = 2.85, ItemName = "Coca Cola"},
                new Products { Id = 08, ImageName = "ColdMeat1", CategoryId = 03, Unit = "1 kg",  Price = 7.50, ItemName = "Macro Free Range Chicken" },
                new Products { Id = 09, ImageName = "ColdMeat2", CategoryId = 03, Unit = "100 g", Price = 3.00, ItemName = "Primo English Ham" },
                new Products { Id = 10, ImageName = "ColdMeat3", CategoryId = 03, Unit = "175 g", Price = 5.00, ItemName = "Primo Short Cut Rindless Bacon" },
                new Products { Id = 11, ImageName = "Dairy1", CategoryId = 04, Unit = "1 Litre", Price = 2.90, ItemName = "A2 Light Milk" },
                new Products { Id = 12, ImageName = "Dairy2", CategoryId = 04, Unit = "100 g",   Price = 4.20, ItemName = "Babybel Mini" },
                new Products { Id = 13, ImageName = "Dairy3", CategoryId = 04, Unit = "250 g",   Price = 4.00, ItemName = "Bega Farmers Tasty" },
                new Products { Id = 14, ImageName = "Dairy4", CategoryId = 04, Unit = "140 g",   Price = 1.50, ItemName = "Chobani Strawbery Yoghurt" },
                new Products { Id = 15, ImageName = "Dairy5", CategoryId = 04, Unit = "250 g", Price = 5.00, ItemName = "Lurpak Salted Blend" },
                new Products { Id = 16, ImageName = "Dairy6", CategoryId = 04, Unit = "6-pk",  Price = 3.60, ItemName = "Manning Valley Eggs" },
                new Products { Id = 17, ImageName = "Dairy7", CategoryId = 04, Unit = "250 g", Price = 4.30, ItemName = "Philadelphia Original Cream Cheese" },
                new Products { Id = 18, ImageName = "Dairy8", CategoryId = 04, Unit = "200 g", Price = 5.00, ItemName = "South Cape Greek Feta" },
                new Products { Id = 19, ImageName = "FreshFoods1", CategoryId = 05, Unit = "1 kg", Price = 2.00, ItemName = "Carrots" },
                new Products { Id = 20, ImageName = "FreshFoods2", CategoryId = 05, Unit = "1 whole", Price = 1.90, ItemName = "Cucumber" },
                new Products { Id = 21, ImageName = "FreshFoods3", CategoryId = 05, Unit = "500 g", Price = 5.90, ItemName = "Fresh Tomato" },
                new Products { Id = 22, ImageName = "FreshFoods4", CategoryId = 05, Unit = "1 kg", Price = 5.50, ItemName = "Granny Smith's Apples" },
                new Products { Id = 23, ImageName = "FreshFoods5", CategoryId = 05, Unit = " 1 whole", Price = 2.50, ItemName = "Iceberg Lettuce" },
                new Products { Id = 24, ImageName = "FreshFoods6", CategoryId = 05, Unit = "1 kg", Price = 3.50, ItemName = "Red Onion" },
                new Products { Id = 25, ImageName = "FreshFoods7", CategoryId = 05, Unit = "1 kg", Price = 4.00, ItemName = "Red Potato Washed" },
                new Products { Id = 26, ImageName = "FreshFoods8", CategoryId = 05, Unit = "1 kg", Price = 4.90, ItemName = "Red Tipped Bananas" },
                new Products { Id = 27, ImageName = "FreshFoods9", CategoryId = 05, Unit = "1 whole", Price = 6.60, ItemName = "Watermelon" },
                new Products { Id = 28, ImageName = "Frozen1", CategoryId = 06, Unit = "375 g", Price = 4.50, ItemName = "Birds Eye Fish Fingers" },
                new Products { Id = 29, ImageName = "Pantry01", CategoryId = 07, Unit = "200 g", Price = 5.00, ItemName = "Cadburry Dairy Milk" },
                new Products { Id = 30, ImageName = "Pantry02", CategoryId = 07, Unit = "500 g", Price = 7.35, ItemName = "Capilano Squeezable Honey" },
                new Products { Id = 31, ImageName = "Pantry03", CategoryId = 07, Unit = "375 ml", Price = 8.00, ItemName = "Cobram Extra Virgin Olive Oil" },
                new Products { Id = 32, ImageName = "Pantry04", CategoryId = 07, Unit = "440 g", Price = 3.25, ItemName = "Golden Circle Pineapple Pieces in natural juice" },
                new Products { Id = 33, ImageName = "Pantry05", CategoryId = 07, Unit = "535 g", Price = 2.50, ItemName = "Heinz Tomato Soup" },
                new Products { Id = 34, ImageName = "Pantry06", CategoryId = 07, Unit = "95 g",  Price = 1.50, ItemName = "John West Tuna can" },
                new Products { Id = 35, ImageName = "Pantry07", CategoryId = 07, Unit = "100 g", Price = 4.25, ItemName = "Lindt Excellence 70% cocoa Block" },
                new Products { Id = 36, ImageName = "Pantry08", CategoryId = 07, Unit = "200 g", Price = 4.00, ItemName = "Milo Chocolate Malt" },
                new Products { Id = 37, ImageName = "Pantry09", CategoryId = 07, Unit = "100 g", Price = 6.00, ItemName = "Moccana Classic Instant Medium Roast" },
                new Products { Id = 38, ImageName = "Pantry10", CategoryId = 07, Unit = "400 g", Price = 4.00, ItemName = "Nutella Jar" },
                new Products { Id = 39, ImageName = "Pantry11", CategoryId = 07, Unit = "420 g", Price = 4.50, ItemName = "Sacla Pasta Tomato Basil Sauce" },
                new Products { Id = 40, ImageName = "Pantry12", CategoryId = 07, Unit = "500 g", Price = 1.95, ItemName = "San Remo Linguine Pasta No. 1" },
                new Products { Id = 41, ImageName = "Pantry13", CategoryId = 07, Unit = "170 g", Price = 3.29, ItemName = "Smith's Original Share Pack Crisps" },
                new Products { Id = 42, ImageName = "Pantry14", CategoryId = 07, Unit = "380 g", Price = 6.00, ItemName = "Vegemite" },
                new Products { Id = 43, ImageName = "SelfCare1", CategoryId = 08, Unit = "110 g", Price = 3.50, ItemName = "Colgate Total Toothpaste Original" }
                );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, ImageCategory = "Baked", CategoryName = "Baked" },
                new ProductCategory { Id = 2, ImageCategory = "Beverage", CategoryName = "Beverage" },
                new ProductCategory { Id = 3, ImageCategory = "ColdMeat", CategoryName = "ColdMeat" },
                new ProductCategory { Id = 4, ImageCategory = "Dairy", CategoryName = "Dairy" },
                new ProductCategory { Id = 5, ImageCategory = "FreshFoods", CategoryName = "FreshFoods" },
                new ProductCategory { Id = 6, ImageCategory = "Frozen", CategoryName = "Frozen" },
                new ProductCategory { Id = 7, ImageCategory = "Pantry", CategoryName = "Pantry" },
                new ProductCategory { Id = 8, ImageCategory = "SelfCare", CategoryName = "SelfCare" }
                );

            modelBuilder.Entity<ShoppingCart>().HasData(
                new ShoppingCart
                {
                    Id = 1,
                    UserId = 1,
                    Date = DateTime.Now,
                    Total = 25.00
                });

            modelBuilder.Entity<ShoppingCartItem>().HasData(
                new ShoppingCartItem
                {
                    Id = 1,
                    ShoppingCartId = 1,
                    ProductsId = 1,
                    Quantity = 1
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Test",
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("1234"),
                    Role = "User1"
                },
                new User
                {
                    Id = 2,
                    UserName = "Test1",
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("1234"),
                    Role = "User2"
                });
        }
    }
}
