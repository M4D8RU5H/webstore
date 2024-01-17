using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace WebStore.Models
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual DateTime OrderDateTime { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual IList<OrderedItem> OrderedItems { get; set; }


        public Order()
        {
            OrderDateTime = DateTime.Now;
            OrderedItems = new List<OrderedItem>();
        }
    }

    public enum OrderStatus
    {
        NEW,
        IN_PROGRESS,
        CANCELED,
        COMPLETED
    }

    public enum PaymentMethod
    {
        CASH_ON_DELLIVERY,
        BANK_TRANSFER,
        BLIK
    }
}
