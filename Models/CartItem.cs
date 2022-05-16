namespace ShoppingCart.Models
{
    public class CartItem : IProduct, ICoupon
    {
        public double DiscountPrice { get; set; }
        public CouponType CouponType { get; set; }
        public double DiscountAmount { get; set; }
        public ProductType ProductType { get; set; }
        public double RetailPrice { get; set; }
        public int OrdinalValue { get; set; }
    }
}