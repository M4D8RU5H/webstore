﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class ItemFileMap : ClassMapping<ItemFile>
    {
        public ItemFileMap()
        {
            Table("ItemFiles");

            Id(x => x.Id,
                m => m.Generator(Generators.Identity));

            Property(x => x.URL);

            ManyToOne(x => x.Item,
                m => m.Column("ItemId"));
        }
    }
}
