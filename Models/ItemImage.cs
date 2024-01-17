namespace WebStore.Models
{
    public class ItemImage
    {
        public virtual int Id { get; set; }
        public virtual string URL { get; set; }
        public virtual Item Item { get; set; }
    }
}
