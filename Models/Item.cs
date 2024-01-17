using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace WebStore.Models
{
    public class Item
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Podaj nazwę przedmiotu", AllowEmptyStrings = false)]
        [DisplayName("Nazwa")]
        [StringLength(100, ErrorMessage = "Nazwa przedmiotu nie może przekraczać 100 znaków")]
        public virtual string Name { get; set; }

        [DisplayName("Opis")]
        [StringLength(15000, MinimumLength = 0,ErrorMessage = "Opis przedmiotu nie może przekraczać 15 000 znaków")]
        public virtual string ?Description { get; set; }

        [Required(ErrorMessage = "Podaj cenę przedmiotu")]
        [DisplayName("Cena")]
        [Range(0, 999999.99, ErrorMessage = "Cena musi mieścić się w przedziale od 0 zł do 999 999.99 zł")]
        [RegularExpression(@"([0-9]+(\,[0-9][0-9])?$)", ErrorMessage = "Cena jest w złym formacie")]
        public virtual decimal Price { get; set; }

        [Required(ErrorMessage = "Określ dostępną ilość przedmiotów w magazynie")]
        [DisplayName("Dostępna ilość")]
        [Range(0, 999999, ErrorMessage = "Dostępna ilość przediotów musi mieścić się w przedziale od 0 do 999 999")]
        public virtual int StockQuantity { get; set; }

        [DisplayName("Data i czas dodania")]
        public virtual DateTime AddedDateTime { get; set; }

        [DisplayName("Widoczność")]
        public virtual bool IsVisible { get; set; }

        [DisplayName("Nowy")]
        public virtual bool IsNew { get; set; }

        [DisplayName("W koszu")]
        public virtual bool IsInRecycleBin { get; set; }

        //[Required(ErrorMessage = "Wybierz kategorię przedmiotu")]
        //TODO: Zrobić walidację kategorii
        [DisplayName("Kategoria")]
        public virtual Category ?Category { get; set; }

        [DisplayName("Oferta specjalna")]
        public virtual SpecialOffer ?SpecialOffer { get; set; }
        public virtual IList<ItemImage> Images { get; set; }

        [Required(ErrorMessage = "Dodaj zdjęcie przedmiotu", AllowEmptyStrings = false)]        
        [DisplayName("Dodaj zdjęcia")]
        //[MaxLength(8, ErrorMessage = "Ilość zdjęć nie może przekraczać 8")]
        public virtual IList<IFormFile> ImageFiles { get; set; } 

        [DisplayName("Załącz pliki")]
        public virtual IList<ItemFile> Files { get; set; }
        public virtual IList<CartItem> CartItems{ get; set; }
        public virtual IList<OrderedItem> OrderedItems { get; set; }


        public Item()
        {
            Images = new List<ItemImage>();
            ImageFiles = new List<IFormFile>();
            Files = new List<ItemFile>();
            CartItems = new List<CartItem>();
            OrderedItems = new List<OrderedItem>(); 
            AddedDateTime = DateTime.Now;
        }
    }
}
