using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Diagnostics.Contracts;
using System.Dynamic;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ViewsController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult LoginOrRegister()
        {
            return View();
        }

        public IActionResult ProductDetails(int id)
        {
            return View();
        }

        public IActionResult AccountManagment()
        {
            return View();
        }
      
        public IActionResult ItemEdit()
        {
            return View();
        }

        public IActionResult CategoryManagment()
        {
           
            var categoryList = new List<Category> { };

            return View(categoryList);
        }

        public IActionResult OrdersManagment()
        {
            return View();
        }

        public IActionResult ItemAdd()
        {
            return View();
        }

        public IActionResult OrdersHistory()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }
        
    }
}
