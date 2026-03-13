namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Models
{
	public class ShoppingCartItem
	{
		public int Id { get; set; }
		public int ShoppingCartId { get; set; }
		public int ProductsId { get; set; }
		public int Quantity { get; set; }

		//Linking references to the other tables that this tbale has a relationship with.
		public virtual ShoppingCart Cart { get; set; }
		public virtual Products ProductsItem { get; set; }

	}
}
