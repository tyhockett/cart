namespace ShoppingCart.Models
{
    public interface ICoupon
    {
        CouponType CouponType { get; set; }
        double DiscountAmount { get; set; }
    }
}