using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class ItemMap : ClassMapping<Item>
    {
        public ItemMap()
        {
            Table("Items");

            Id(x => x.Id,
                m => m.Generator(Generators.Identity));

            Property(x => x.Name);
            Property(x => x.Description);
            Property(x => x.Price);
            Property(x => x.StockQuantity);
            Property(x => x.AddedDateTime);
            Property(x => x.IsVisible);
            Property(x => x.IsNew);
            Property(x => x.IsInRecycleBin);

            ManyToOne(x => x.Category,
                m => m.Column("CategoryId"));
            
            OneToOne(x => x.SpecialOffer, m => m.Cascade(Cascade.All));
            
            Bag(x => x.Images,
                m => {
                    m.Cascade(Cascade.All);
                    m.Key(k => k.Column("ItemId"));
                },
                r => r.OneToMany(m => m.Class(typeof(ItemImage))));
            
            Bag(x => x.Files,
                m => {
                    m.Cascade(Cascade.All);
                    m.Key(k => k.Column("ItemId"));
                },
                r => r.OneToMany(m => m.Class(typeof(ItemFile))));
            
            Bag(x => x.CartItems,
                m => {
                    m.Cascade(Cascade.All);
                    m.Key(k => k.Column("ItemId"));
                },
                r => r.OneToMany(m => m.Class(typeof(CartItem)))); //Potrzebne? Czy muszę wiedzieć w jakich wózkach użytkowników jest dany przedmiot? Nienaturalna relacja
            
            Bag(x => x.OrderedItems,
                m => {
                    m.Cascade(Cascade.None);
                    m.Key(k => k.Column("ItemId"));
                },
                r => r.OneToMany(m => m.Class(typeof(OrderedItem))));
        }
    }
}
