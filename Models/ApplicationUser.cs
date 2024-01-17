using NHibernate.AspNetCore.Identity;

namespace WebStore.Models
{
    public class ApplicationUser : IdentityUser
    {    
        
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Street { get; set; }
        public virtual string BuildingNumber { get; set; }
        public virtual string ApartmentNumber { get; set; }

        public virtual IList<CartItem> CartItems { get; set; }
        public virtual IList<Order> Orders { get; set; }


        public ApplicationUser()
        {
            CartItems = new List<CartItem>();
            Orders = new List<Order>();
        }
    }
}
