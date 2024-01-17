using Microsoft.AspNetCore.Identity;

using ISession = NHibernate.ISession;
using NHibernate.AspNetCore.Identity;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.NetCore;
using NHibernate;

using WebStore.Models;
using WebStore.Persistence.NHibernate.Mappings;
using IdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;
using NHibernate.AspNetCore.Identity.Mappings;

public static class NHiberanteSqlServerInstaller
{   
    private static ISessionFactory sessionFactory;

    /*
    public static ISession OpenSession()
    {
        return sessionFactory.OpenSession();
    }
    */

    public static IServiceCollection AddNHibernateSqlServer(this IServiceCollection services, string cnString)
    {
        var cfg = new Configuration();

        cfg.DataBaseIntegration(db =>
        {
            db.Dialect<MsSql2012Dialect>();
            db.Driver<MicrosoftDataSqlClientDriver>();
            db.ConnectionProvider<DriverConnectionProvider>();
            db.LogSqlInConsole = true;
            db.ConnectionString = cnString;
            db.Timeout = 30;/*seconds*/
            //db.SchemaAction = SchemaAutoAction.Validate;
        });

        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddHibernateStores();

        cfg.Cache(c => c.UseQueryCache = false);

        var mapping = new ModelMapper();
        mapping.AddMappings(typeof(ApplicationUserMap).Assembly.GetTypes());

        mapping.AddMapping(typeof(IdentityUserMappingMsSql));
        mapping.AddMapping(typeof(IdentityRoleClaimMappingMsSql));
        mapping.AddMapping(typeof(IdentityRoleMappingMsSql));
        mapping.AddMapping(typeof(IdentityUserClaimMappingMsSql));
        mapping.AddMapping(typeof(IdentityUserLoginMappingMsSql));
        mapping.AddMapping(typeof(IdentityUserRoleMappingMsSql));
        mapping.AddMapping(typeof(IdentityUserTokenMappingMsSql)); 
        
        //mapping.AddMapping(typeof(CategoryMap)); //Potrzebne?

        var mappingDocument = mapping.CompileMappingForAllExplicitlyAddedEntities();
        cfg.AddMapping(mappingDocument);     
        
        services.AddHibernate(cfg);

#if DEBUG
        sessionFactory = cfg.BuildSessionFactory();
#endif

        services.AddTransient<ISession>(provider => { 
            return provider.GetService<ISessionFactory>()!.OpenSession(); });

        return services;
    }
}