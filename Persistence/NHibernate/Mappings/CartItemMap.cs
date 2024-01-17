using NHibernate.Mapping.ByCode.Conformist;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class CartItemMap : ClassMapping<CartItem>
    {
        public CartItemMap()
        {
            Table("CartItems");

            ComposedId(m => {
                m.ManyToOne(x => x.User, m => m.Column("UserId"));
                m.ManyToOne(x => x.Item, m => m.Column("ItemId"));
             });

            Property(x => x.Quantity);
        }
    }
}
