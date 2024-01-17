using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebStore.Models
{
    public class SpecialOffer
    {
        public virtual int ItemId { get; set; }
        public virtual Item Item { get; set; }

        [DisplayName("Poprzednia cena")]
        public virtual decimal PreviousPrice { get; set; }
    }
}
