namespace WebStore.ViewModels.CartItemViewModels
{
    public class CartViewModel
    {
        public IList<CartItemViewModel> CartItems { get; set; }
        public string CartValue { get; set; }


        public CartViewModel()
        {
            CartItems = new List<CartItemViewModel>();
        }
    }
}
