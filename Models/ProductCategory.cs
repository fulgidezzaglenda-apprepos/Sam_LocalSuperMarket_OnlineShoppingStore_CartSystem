namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null;
        public string? ImageCategory { get; set; }

        public virtual List<Products> Products { get; set; } = null;
    }
}
