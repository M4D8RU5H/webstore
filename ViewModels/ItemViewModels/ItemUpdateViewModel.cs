namespace WebStore.ViewModels.ItemViewModels
{
    public class ItemUpdateViewModel
    {
        public string Name { get; set; }
        public string ?Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsVisible { get; set; }
        public int CategoryId { get; set; }         
        public List<string> ImageURLs { get; set; }

        public ItemUpdateViewModel()
        {
            ImageURLs = new List<string>();
        }
    }
}
