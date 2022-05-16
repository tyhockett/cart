namespace ShoppingCart.Models
{
    public interface IProduct
    {
        ProductType ProductType { get; set; }
        double RetailPrice { get; set; }
    }
}