using WebStore.Models;

namespace WebStore.ViewModels.ItemViewModels
{
    public class HomePageItemsViewModel
    {
        public bool displayHome { get; set; }
        public IList<Category> categories { get; set; }

        public IList<ItemOverviewViewModel> ItemsByCategory { get; set; }
        public IList<ItemOverviewViewModel> NewItems { get; set; }
        public IList<ItemOverviewViewModel> SpecialOfferItems { get; set; }       
        public IList<ItemOverviewViewModel> TopTenItems { get; set; }


        public HomePageItemsViewModel()
        {
            displayHome = true;
            categories = new List<Category>();
            ItemsByCategory = new List<ItemOverviewViewModel>();
            NewItems = new List<ItemOverviewViewModel>();
            SpecialOfferItems = new List<ItemOverviewViewModel>();
            TopTenItems = new List<ItemOverviewViewModel>();
        }
    }
}
