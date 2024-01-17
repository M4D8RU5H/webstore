namespace WebStore.Models
{
    public class OrderedItem
    {
        public virtual int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Item Item { get; set; }
        public virtual int Quantity { get; set; }


        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
