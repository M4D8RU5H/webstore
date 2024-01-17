namespace WebStore.ViewModels.ApplicationUserViewModels
{
    public class UserAccountManagementViewModel
    {
        public UserEditEmail EditEmail { get; set; }
        public UserEditPassword EditPassword { get; set; }
        public UserEditShippingAddressViewModel EditShippingAddress { get; set; }

        public UserAccountManagementViewModel()
        {
            EditEmail = new UserEditEmail();
            EditPassword = new UserEditPassword();
            EditShippingAddress = new UserEditShippingAddressViewModel();
        }
    }
}
