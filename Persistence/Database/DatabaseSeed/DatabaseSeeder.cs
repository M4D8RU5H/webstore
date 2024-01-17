using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using WebStore.Models;
using IdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;
using ISession = NHibernate.ISession;

namespace WebStore.Persistence.Database.DatabaseSeed
{
    public class DatabaseSeeder
    {
        public static async Task InitializeDatabaseWithSampleData(ISession session, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostEnvironment)
        {
            if(IsDatabaseEmpty(session))
            {
                var transaction = session.BeginTransaction();

                /*CATEGORIES*/
                var categories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Dla dzieci",
                        IsVisible = true,
                        ChildCategories = new List<Category>()
                        {
                            new Category()
                            {
                                Name = "Zabawki",
                                IsVisible = true
                            }
                        }
                    },

                    new Category()
                    {
                        Name = "Zdrowie i uroda",
                        IsVisible = true
                    },

                    new Category()
                    {
                        Name = "Elektronika",
                        IsVisible = true,
                        ChildCategories = new List<Category>()
                        {
                            new Category()
                            {
                                Name = "Komputery",
                                IsVisible = true
                            }
                        }
                    }
                };

                foreach (var cat in categories) { session.Save(cat); }


                /*ITEMS*/
                var items = new List<Item>()
                {
                    new Item()
                    {
                        Name = "Granatnik Szymczyka",
                        Description = "Lekki i poręczny granatnik typu RGW-90. Możesz pochwalić się nim koledze. INSTRUKCJA UŻYCIA: 1. Upewnij się, że granatnik jest naładowany. 2. Wyceluj w drzwi i pociągni za spust. 3. Ciesz się sianym spustoszeniem",
                        Price = Decimal.Parse("6,40"),
                        StockQuantity = 1,
                        AddedDateTime = DateTime.Now,
                        IsVisible = true,
                        IsNew = true,
                        IsInRecycleBin = false,
                        Category = session.Query<Category>().Where(c => c.Name == "Zabawki").FirstOrDefault(),
                        Images = new List<ItemImage>()
                        {
                            new ItemImage() { URL = hostEnvironment.WebRootPath + "\\images\\item_images\\" + "granatnik.jpg" }
                        }
                    },

                    new Item()
                    {
                        Name = "Perełka",
                        Description = "Napój Bogów i studentów",
                        Price = Decimal.Parse("2,70"),
                        StockQuantity = 1000,
                        AddedDateTime = DateTime.Now,
                        IsVisible = true,
                        IsNew = true,
                        IsInRecycleBin = false,
                        Category = session.Query<Category>().Where(c => c.Name == "Zdrowie i uroda").FirstOrDefault(),
                        Images = new List<ItemImage>()
                        {
                            new ItemImage() { URL = hostEnvironment.WebRootPath + "\\images\\item_images\\" + "perelka.png" }
                        }
                    },

                    new Item()
                    {
                        Name = "Cygański laptop czterordzeniowy",
                        Description = "Wydajny i szybki, cieszący oko prostym ale gustownym designem",
                        Price = Decimal.Parse("2700,00"),
                        StockQuantity = 6,
                        AddedDateTime = DateTime.Now,
                        IsVisible = true,
                        IsNew = true,
                        IsInRecycleBin = false,
                        Category = session.Query<Category>().Where(c => c.Name == "Komputery").FirstOrDefault(),
                        Images = new List<ItemImage>()
                        {
                            new ItemImage() { URL = hostEnvironment.WebRootPath + "\\images\\item_images\\" + "laptop.PNG" }
                        }
                    }
                };

                foreach (var item in items)
                {
                    item.Images[0].Item = item;
                    session.Save(item);
                }


                /*USERS*/
                var regularUser = new ApplicationUser()
                {
                    Email = "user@webstore.pl",
                    UserName = "user@webstore.pl",
                    FirstName = "User",
                    LastName = "Ógułem",
                    City = "Białystok",
                    PostalCode = "15-640",
                    Street = "Szkolna",
                    BuildingNumber = "17",
                    ApartmentNumber = null,
                };

                var adminUser = new ApplicationUser()
                {
                    Email = "admin@webstore.pl",
                    UserName = "admin@webstore.pl",
                    FirstName = "Admin",
                    LastName = "Psikuta",
                    City = "City",
                    PostalCode = "12-345",
                    Street = "Street",
                    BuildingNumber = "1",
                    ApartmentNumber = "2"
                };

                var result = userManager.CreateAsync(regularUser, "User123!").GetAwaiter().GetResult();
                result = userManager.CreateAsync(adminUser, "Admin123!").GetAwaiter().GetResult();


                /*ROLES*/
                var regularUserRole = new IdentityRole() { Name = "Regular User" };
                var adminRole = new IdentityRole() { Name = "Administrator" };

                result = roleManager.CreateAsync(regularUserRole).GetAwaiter().GetResult();
                result = roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();


                /*USER ROLES*/
                result = userManager.AddToRoleAsync(regularUser, "Regular User").GetAwaiter().GetResult();
                result = userManager.AddToRoleAsync(adminUser, "Administrator").GetAwaiter().GetResult();


                /*CART ITEMS*/
                var cartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        //Item = session.Query<Item>().Where(i => i.Name == "Granatnik Szymczyka"),
                        Item = items[0],
                        Quantity = 1,
                    }
                };

                foreach (var cartItem in cartItems)
                {
                    cartItem.User = regularUser;
                    session.Save(cartItem);
                }

                /*SHIPPING METHODS*/
                var shippingMethod = new ShippingMethod()
                {
                    Method = ShippingMethodEnum.HOME_DELIVERY,
                    Price = Decimal.Parse("15,99")
                };

                session.Save(shippingMethod);


                /*ORDERS*/
                var orders = new List<Order>()
                {
                    new Order()
                    {
                        Status = OrderStatus.COMPLETED,
                        PaymentMethod = PaymentMethod.CASH_ON_DELLIVERY,
                        ShippingMethod = shippingMethod,

                        OrderedItems = new List<OrderedItem>()
                        {
                            new OrderedItem()
                            {
                                Item = items[1],
                                Quantity = 100
                            },

                            new OrderedItem()
                            {
                                Item = items[2],
                                Quantity = 1
                            }
                        }
                    }
                };

                foreach (var order in orders)
                {
                    order.User = regularUser;

                    foreach(var orderedItem in order.OrderedItems)
                    {
                        orderedItem.Order = order;
                    }
                    session.SaveOrUpdate(order);
                }

                transaction.Commit();
            }
        }

        private static bool IsDatabaseEmpty(ISession session)
        {
            if (session.Query<ApplicationUser>().Any()) { return false; }
            if (session.Query<IdentityRole>().Any()) { return false; }
            if (session.Query<CartItem>().Any()) { return false; }
            if (session.Query<Category>().Any()) { return false; }
            if (session.Query<Item>().Any()) { return false; }
            if (session.Query<ItemFile>().Any()) { return false; }
            if (session.Query<ItemImage>().Any()) { return false; }
            if (session.Query<Order>().Any()) { return false; }
            if (session.Query<OrderedItem>().Any()) { return false; }
            if (session.Query<ShippingMethod>().Any()) { return false; }
            if (session.Query<SpecialOffer>().Any()) { return false; }

            return true;
        }        
    }
}
