using System;
using ShoppingCart.Models;
using ShoppingCart.Services;

namespace ShoppingCart
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var cartService = new CartService();
            var couponService = new CouponService();
            var productService = new ProductService();

            var cart = cartService.GenerateCart();

            cartService.AddProduct(cart,
                productService.GetProduct(ProductType.BusinessCard, 13.00));

            cartService.AddProduct(cart,
                productService.GetProduct(ProductType.Hoodie, 47.00));

            cartService.AddCoupon(cart,
                couponService.GetCoupon(CouponType.PercentageOffEntireCart, 0.15));

            cartService.AddProduct(cart,
                productService.GetProduct(ProductType.Shirt, 21.00));

            cartService.AddProduct(cart,
                productService.GetProduct(ProductType.BusinessCard, 11.00));

            cartService.AddCoupon(cart,
                couponService.GetCoupon(CouponType.PercentageOffNextItem, 0.20));

            cartService.AddCoupon(cart,
                couponService.GetCoupon(CouponType.DollarsOffNextItemOfType, 5.00, ProductType.Shirt));

            cartService.AddProduct(cart,
                productService.GetProduct(ProductType.Hoodie, 53.00));

            cartService.AddProduct(cart,
                productService.GetProduct(ProductType.Shirt, 26.00));

            cartService.AddProduct(cart,
                productService.GetProduct(ProductType.Shirt, 31.00));

            Console.WriteLine($"Total Price: {cart.TotalPrice():C}");
        }
    }
}