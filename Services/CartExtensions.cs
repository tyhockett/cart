using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public static class CartExtensions
    {
        public static double TotalPrice(this Cart cart)
        {
            var cartService = new CartService();
            return cartService.GetTotalPrice(cart);
        }
    }
}