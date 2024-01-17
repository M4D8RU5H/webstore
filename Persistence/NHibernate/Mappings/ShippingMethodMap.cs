using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class ShippingMethodMap : ClassMapping<ShippingMethod>
    {
        public ShippingMethodMap()
        {
            Table("ShippingMethods");

            Id(x => x.Id,
                m => m.Generator(Generators.Identity));

            Property(x => x.Method);
            Property(x => x.Price);

            //HasMany(x => x.Orders);
            // .Access.ReadOnly()

            Bag(x => x.Orders,
                m => {
                    m.Cascade(Cascade.None);
                    m.Key(k => k.Column("ShippingMethodId"));
                },
                r => r.OneToMany(m => m.Class(typeof(Order))));
        }
    }
}
