namespace WebStore.ViewModels.CategoryViewModels
{
    public class CategoryBrowserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public bool HasChildren { get; set; }
    }
}
