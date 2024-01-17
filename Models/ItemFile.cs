namespace WebStore.Models
{
    public class ItemFile
    {
        public virtual int Id { get; set; }
        public virtual string URL { get; set; }
        public virtual Item Item { get; set; }
    }
}
