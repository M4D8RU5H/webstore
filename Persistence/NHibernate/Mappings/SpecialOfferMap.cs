using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class SpecialOfferMap : ClassMapping<SpecialOffer>
    {
        public SpecialOfferMap()
        {
            Table("SpecialOffers");

            Id(x => x.ItemId,
                m => m.Generator(Generators.Foreign(typeof(Item))));

            Property(x => x.PreviousPrice);

            OneToOne(x => x.Item, m => m.Cascade(Cascade.None));
        }
    }
}
