namespace WebStore.ViewModels.ItemViewModels
{
    public class ItemDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public List<string> ImageURLs { get; set; }

        public ItemDetailsViewModel()
        {
            ImageURLs = new List<string>();
        }
    }
}
