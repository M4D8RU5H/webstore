namespace WebStore.Models
{
    public class ShippingMethod
    {
        public virtual int Id { get; set; }
        public virtual ShippingMethodEnum Method { get; set; }
        public virtual decimal Price { get; set; }
        public virtual IList<Order> Orders { get; set; }


        public ShippingMethod()
        {
            Orders = new List<Order>();
        }
    }

    public enum ShippingMethodEnum
    {
        INPOST_PARCEL_LOCKER,
        HOME_DELIVERY,
    }
}
