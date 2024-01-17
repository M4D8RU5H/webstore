using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using WebStore.Models;
using WebStore.Other;
using WebStore.Persistence.Repositories.WorkUnit;
using WebStore.ViewModels.CartItemViewModels;

namespace WebStore.Controllers
{
    public class CartItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CartItemController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var cartItems = currentUser.CartItems;

            if (!cartItems.IsNullOrEmpty())
            {
                var cartVM = new CartViewModel();
                decimal cartValue = 0;

                foreach (var cartItem in cartItems)
                {
                    var cartItemVM = new CartItemViewModel()
                    {
                        ItemName = cartItem.Item.Name,
                        ItemImageURL = "../" + PathModifier.MakeRelativePath(cartItem.Item.Images.FirstOrDefault().URL, _hostEnvironment.WebRootPath + "/wwwroot", '/'),
                        ItemPrice = decimal.Round(cartItem.Item.Price, 2, MidpointRounding.AwayFromZero).ToString() + " zł",
                        Quantity = cartItem.Quantity
                    };

                    cartValue += cartItem.Quantity * cartItem.Item.Price;

                    cartVM.CartItems.Add(cartItemVM);
                }

                cartVM.CartValue = decimal.Round(cartValue, 2, MidpointRounding.AwayFromZero).ToString() + " zł";

                return View(cartVM);
            }

            return View();
        }

        public async Task<IActionResult> AddToCart(int itemId)
        {
            var item = await _unitOfWork.Items.Get(itemId);

            if (item == null)
            {
                return BadRequest();
            }

            var cartItem = new CartItem()
            {
                User = await _userManager.GetUserAsync(HttpContext.User),
                Item = item,
                Quantity = 1
            };

            await _unitOfWork.CartItems.Add(cartItem);
            await _unitOfWork.Complete();

            return NoContent();
        }

        public void DeleteFromCart(CartItem item) {
            //this.cartItems.Remove(item); 
        }

        public void ChangeQuantity(String button, CartItem item)
        {

            if (item.Quantity > 1)
            {
                if (button == "plus"){ item.Quantity += 1;}

                else{ item.Quantity -= 1; }
            }

        }       
    }
}

