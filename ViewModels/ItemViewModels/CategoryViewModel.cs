using WebStore.Models;

namespace WebStore.ViewModels.ItemViewModels
{
    public class CategoryViewModel
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual IList<Category> ChildrenCategories { get; set; }
    }
}
