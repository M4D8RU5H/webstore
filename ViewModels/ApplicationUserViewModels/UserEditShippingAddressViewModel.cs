using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.ApplicationUserViewModels
{
    public class UserEditShippingAddressViewModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        [StringLength(20, ErrorMessage = "Imię nie może mieć więcej niż 10 znaków")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        [StringLength(20, ErrorMessage = "Nazwisko nie może mieć więcej niż 10 znaków")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone]
        [Display(Name = "Numer telefonu")]
        public string Phone { get; set; }


        [DataType(DataType.PostalCode, ErrorMessage = "Wprowadzono błędny kod pocztowy")]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }


        [Display(Name = "Miasto")]
        public string City { get; set; }


        [Display(Name = "Ulica")]
        public string Street { get; set; }


        [Display(Name = "Numer budynku")]
        public string BuildingNumber { get; set; }

        [Display(Name = "Numer mieszkania")]
        public string? ApartmentNumber { get; set; }
    }
}
