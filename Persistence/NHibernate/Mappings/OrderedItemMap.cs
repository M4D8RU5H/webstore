using NHibernate.Mapping.ByCode.Conformist;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class OrderedItemMap : ClassMapping<OrderedItem>
    {
        public OrderedItemMap()
        {
            Table("OrderedItems");

            ComposedId(m => {
                m.ManyToOne(x => x.Order, m => m.Column("OrderId"));
                m.ManyToOne(x => x.Item, m => m.Column("ItemId"));
            });

            Property(x => x.Quantity);
        }
    }
}
