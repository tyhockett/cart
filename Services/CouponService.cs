using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class CouponService
    {
        public Coupon GetCoupon(CouponType couponType, double discountAmount, ProductType? productType = null)
        {
            return new Coupon
            {
                CouponType = couponType,
                DiscountAmount = discountAmount,
                ProductType = productType
            };
        }

        public void ApplyPercentageOff(CartItem cartItem, double amount)
        {
            var result = cartItem.DiscountPrice - cartItem.DiscountPrice * amount;

            Console.WriteLine($" * Taking {amount * 100}% off {cartItem.ProductType}: was {cartItem.DiscountPrice:C}, now {result:C}");

            cartItem.DiscountPrice = result;
        }

        public void ApplyDollarsOff(CartItem cartItem, double amount)
        {
            var result = cartItem.DiscountPrice - amount;

            Console.WriteLine($" * Taking {amount:C} off {cartItem.ProductType}: was {cartItem.DiscountPrice:C}, now {result:C}");

            cartItem.DiscountPrice = result;
        }

        public void ApplyPercentageOff(IEnumerable<CartItem> cartItems, double amount)
        {
            var itemsToDiscount = cartItems.Where(x => x.CouponType == CouponType.None);

            foreach (var cartItem in itemsToDiscount) ApplyPercentageOff(cartItem, amount);
        }

        public CartItem GetCouponPercentageOffEntireCart(IEnumerable<CartItem> cartItems)
        {
            return cartItems.FirstOrDefault(x => x.CouponType == CouponType.PercentageOffEntireCart);
        }

        public List<CartItem> GetOrdinalCoupons(IEnumerable<CartItem> cartItems)
        {
            return cartItems.Where(x =>
                x.CouponType == CouponType.PercentageOffNextItem ||
                x.CouponType == CouponType.DollarsOffNextItemOfType).ToList();
        }

        public void ApplyPercentageOffNextItem(List<CartItem> cartItems, double amount, int ordinalValue)
        {
            var cartItem = cartItems.FirstOrDefault(x =>
                x.CouponType == CouponType.None && x.OrdinalValue >= ordinalValue);

            if (cartItem != null) ApplyPercentageOff(cartItem, amount);
        }

        public void ApplyDollarsOffNextItemOfType(List<CartItem> cartItems, double amount, int ordinalValue,
            ProductType productType)
        {
            var cartItem = cartItems.FirstOrDefault(x =>
                x.CouponType == CouponType.None &&
                x.OrdinalValue >= ordinalValue &&
                x.ProductType == productType);

            if (cartItem != null) ApplyDollarsOff(cartItem, amount);
        }
    }
}