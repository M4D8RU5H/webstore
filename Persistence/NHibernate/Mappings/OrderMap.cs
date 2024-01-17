using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Table("Orders");

            Id(x => x.Id,
                m => m.Generator(Generators.Identity));

            Property(x => x.Status);
            Property(x => x.PaymentMethod);
            Property(x => x.OrderDateTime);

            ManyToOne(x => x.User,
                m => m.Column("UserId"));

            ManyToOne(x => x.ShippingMethod, 
                m => m.Column("ShippingMethodId"));

            Bag(x => x.OrderedItems,
                m => {
                    m.Cascade(Cascade.All);                    
                    m.Key(k => k.Column("OrderId"));
                },
                r => r.OneToMany(m => m.Class(typeof(OrderedItem))));
        }
    }
}
