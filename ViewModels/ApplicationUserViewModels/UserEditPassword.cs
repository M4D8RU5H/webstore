using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.ApplicationUserViewModels
{
    public class UserEditPassword
    {
        [Required, DataType(DataType.Password), Display(Name ="Aktualne hasło")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Podano różne hasła")]
        public string ConfirmNewPassword { get; set; }
    }
}
