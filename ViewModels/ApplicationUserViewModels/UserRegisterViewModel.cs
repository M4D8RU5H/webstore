using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.ApplicationUserViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Podaj {0}")]
        [EmailAddress(ErrorMessage = "{0} nie jest prawidłowy")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Podaj {0}")]
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej {2} znaków", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{1,}$", ErrorMessage = "{0} musi zawierać:<br> - małe litery,<br> - duże litery,<br> - cyfry,<br> - znaki specjalne")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        public string ConfirmPassword { get; set; }
    }
}
