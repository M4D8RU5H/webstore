namespace WebStore.Models
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual Category ParentCategory { get; set; }  
        public virtual IList<Category> ChildCategories { get; set; }
        public virtual IList<Item> Items { get; set; }


        public Category()
        {
            ChildCategories = new List<Category>();
            Items = new List<Item>();
        }
    }
}
