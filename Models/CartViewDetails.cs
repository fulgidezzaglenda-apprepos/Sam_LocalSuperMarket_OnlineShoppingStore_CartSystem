namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Models
{
    public class CartViewDetails
    {
        public IEnumerable<ShoppingCart> FinalizedCarts { get; set; } = null;
        public IEnumerable<ShoppingCart> CancelledCarts { get; set; } = null;
    }
}
