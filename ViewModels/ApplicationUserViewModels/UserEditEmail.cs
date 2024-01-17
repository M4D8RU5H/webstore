using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.ApplicationUserViewModels
{
    public class UserEditEmail
    {
        [Required(ErrorMessage = "Podaj {0}")]
        [EmailAddress(ErrorMessage = "{0} nie jest prawidłowy")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
