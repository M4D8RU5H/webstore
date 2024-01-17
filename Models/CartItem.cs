using MessagePack.Formatters;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System.ComponentModel;

namespace WebStore.Models
{
    public class CartItem
    {
        public virtual ApplicationUser User { get; set; }
        public virtual Item Item { get; set; }
        public virtual int Quantity { get; set; }


        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
