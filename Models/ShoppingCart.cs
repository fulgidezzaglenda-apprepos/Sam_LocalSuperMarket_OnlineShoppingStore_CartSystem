using Sam_LocalSuperMarket_OnlineShoppingStore1.Data;

namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Models
{
	public class ShoppingCart
	{
		public int Id { get; set; }
		public int UserId { get; set; }

		//TODO: Fix this later
		public DateTime? Date { get; set; }
		public double? Total { get; set; }

		//Tracks whether the user has completed the checkout on the cart.
		public bool IsFinalised { get; set; } = false;
		public bool IsCancelled { get; set; } = false;

		//Linking references to the other tables that this table has a relationship with.
		public virtual User CartUser { get; set; } = null;
		public virtual List<ShoppingCartItem> CartItems { get; set; } = null;
    }
}
