using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class ProductService
    {
        public Product GetProduct(ProductType productType, double retailPrice)
        {
            return new Product
            {
                ProductType = productType,
                RetailPrice = retailPrice
            };
        }
    }
}