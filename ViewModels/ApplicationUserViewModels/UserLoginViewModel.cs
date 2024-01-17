using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.ApplicationUserViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Podaj {0}")]
        [EmailAddress(ErrorMessage = "{0} nie jest prawidłowy")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Podaj {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}
