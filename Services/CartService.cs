using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class CartService
    {
        public Cart GenerateCart()
        {
            return new Cart
            {
                CartId = Guid.NewGuid(),
                Items = new List<CartItem>()
            };
        }

        public void AddProduct(Cart cart, Product product)
        {
            cart.Items.Add(new CartItem
            {
                CouponType = CouponType.None,
                DiscountAmount = 0,
                DiscountPrice = product.RetailPrice,
                ProductType = product.ProductType,
                RetailPrice = product.RetailPrice,
                OrdinalValue = cart.Items.Count
            });
        }

        public void AddCoupon(Cart cart, Coupon coupon)
        {
            cart.Items.Add(new CartItem
            {
                CouponType = coupon.CouponType,
                DiscountAmount = coupon.DiscountAmount,
                DiscountPrice = 0,
                ProductType = coupon.ProductType ?? ProductType.None,
                RetailPrice = 0,
                OrdinalValue = cart.Items.Count
            });
        }

        public double GetTotalPrice(Cart cart)
        {
            var couponService = new CouponService();

            Console.WriteLine($"Total Before Discounts: {cart.Items.Sum(x => x.RetailPrice):C}");

            var coupon = couponService.GetCouponPercentageOffEntireCart(cart.Items);

            if (coupon != null)
            {
                couponService.ApplyPercentageOff(cart.Items, coupon.DiscountAmount);
            }

            Console.WriteLine($"Total After PercentageOffEntireCart: {cart.Items.Sum(x => x.DiscountPrice):C}");

            var ordinalCoupons = couponService.GetOrdinalCoupons(cart.Items);

            foreach (var ordinalCoupon in ordinalCoupons)
            {
                switch (ordinalCoupon.CouponType)
                {
                    case CouponType.PercentageOffNextItem:
                        couponService.ApplyPercentageOffNextItem(cart.Items, ordinalCoupon.DiscountAmount, ordinalCoupon.OrdinalValue + 1);
                        break;

                    case CouponType.DollarsOffNextItemOfType:
                        couponService.ApplyDollarsOffNextItemOfType(cart.Items, ordinalCoupon.DiscountAmount, ordinalCoupon.OrdinalValue + 1, ordinalCoupon.ProductType);
                        break;
                }
            }

            Console.WriteLine($"Total After Ordinal Coupons: {cart.Items.Sum(x => x.DiscountPrice):C}");

            return cart.Items.Sum(x => x.DiscountPrice);
        }
    }
}