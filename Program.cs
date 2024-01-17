using Microsoft.AspNetCore.Identity;
using WebStore.Models;
using WebStore.Persistence.Database.DatabaseSeed;
using WebStore.Persistence.Repositories.WorkUnit;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("DefaultConnection");
builder.Services.AddNHibernateSqlServer(connectionString);

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add UnitOfWork dependency injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


/*Insert sample data to database
  Works only when the database exists and is empty!

  Admin user: 
    - email: admin@webstore.pl
    - password: Admin123!

  Regular user:
    - email: user@webstore.pl
    - password: User123!
*/

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    var session = services.GetRequiredService<NHibernate.ISession>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<NHibernate.AspNetCore.Identity.IdentityRole>>();
    var hostEnvironment  = services.GetRequiredService<IWebHostEnvironment>();

    // DatabaseSeeder.InitializeDatabaseWithSampleData(session, userManager, roleManager, hostEnvironment).Wait();
}

app.Run();
