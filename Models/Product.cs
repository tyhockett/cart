namespace ShoppingCart.Models
{
    public class Product : IProduct
    {
        public ProductType ProductType { get; set; }
        public double RetailPrice { get; set; }
    }
}