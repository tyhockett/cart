namespace ShoppingCart.Models
{
    public class Coupon : ICoupon
    {
        public CouponType CouponType { get; set; }
        public double DiscountAmount { get; set; }
        public ProductType? ProductType { get; set; }
    }
}