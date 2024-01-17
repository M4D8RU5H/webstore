using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class ApplicationUserMap : JoinedSubclassMapping<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            Table("ApplicationUsers");

            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.City);
            Property(x => x.PostalCode);
            Property(x => x.Street);
            Property(x => x.BuildingNumber);
            Property(x => x.ApartmentNumber);

            Bag(x => x.CartItems,
                m => {
                    m.Cascade(Cascade.All);
                    m.Key(k => k.Column("UserId"));
                },
                r => r.OneToMany(m => m.Class(typeof(CartItem))));

            Bag(x => x.Orders,
                m => {
                    m.Cascade(Cascade.None);
                    m.Key(k => k.Column("UserId"));
                },
                r => r.OneToMany(m => m.Class(typeof(Order))));
        }
    }
}
