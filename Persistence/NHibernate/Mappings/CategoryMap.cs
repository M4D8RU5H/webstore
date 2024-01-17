using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebStore.Models;

namespace WebStore.Persistence.NHibernate.Mappings
{
    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Table("Categories");

            Id(x => x.Id,
                m => m.Generator(Generators.Identity));

            Property(x => x.Name);
            Property(x => x.IsVisible);

            ManyToOne(x => x.ParentCategory,
                m => { 
                    m.Column("ParentCategoryId");                   
                    //m.Cascade(Cascade.All);    //Żeby dodając nową kategorię rodzica dla dziecka dodać tą kategorię również w bazie danych          
                });       

            Bag(x =>x.ChildCategories,
                m => { 
                    m.Cascade(Cascade.All); //Żeby wraz z usunięciem kategori rodzica uswać jego dzieci oraz żeby modyfikując kategorie dzieci rodzica modyfikować te kategorie również w bazie danych
                    m.Key(k => k.Column("ParentCategoryId")); //Kolumna w tabeli drugiej strony relacji
                },
                r => r.OneToMany(m => m.Class(typeof(Category))));

            Bag(x => x.Items,
                m => {
                    m.Cascade(Cascade.None);
                    m.Key(k => k.Column("CategoryId"));
                },
                r => r.OneToMany(m => m.Class(typeof(Item))));
        }
    }
}
