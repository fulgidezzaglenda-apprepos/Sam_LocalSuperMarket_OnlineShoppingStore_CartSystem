namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = null;
        public string Unit { get; set; } = null;
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string? ImageName { get; set; }

		//Linking references to the other tables that this tables has a relationship with.
		public virtual ProductCategory Category { get; set; } = null;
        public virtual List<ShoppingCartItem> CartItems { get; set; } = null; 
    }
}
