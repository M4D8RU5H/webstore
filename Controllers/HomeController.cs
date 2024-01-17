using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using Microsoft.AspNetCore.Identity;
using WebStore.ViewModels.ItemViewModels;
using WebStore.Persistence.Repositories.WorkUnit;
using WebStore.Other;
using WebStore.Persistence.Repositories.CategoryRepo;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var homePageVM = new HomePageItemsViewModel();

            var rootCategories = await _unitOfWork.Categories.GetRootCategories();

            foreach (var category in rootCategories)
            {
                var categoryVM = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsVisible = category.IsVisible,
                    ChildCategories = category.ChildCategories
                };

                homePageVM.categories.Add(categoryVM);
            }

            var newItems = await _unitOfWork.Items.Find(i => i.IsNew == true);

            // New Items
            foreach (var item in newItems) 
            {
                var itemOverview = new ItemOverviewViewModel()
                {
                    Name = item.Name,
                    Price = decimal.Round(item.Price, 2, MidpointRounding.AwayFromZero).ToString() + " zł",
                    ImageURL = PathModifier.MakeRelativePath(item.Images.FirstOrDefault()!.URL, _hostEnvironment.WebRootPath + "/wwwroot", '/')
                };

                homePageVM.NewItems.Add(itemOverview);
            }


            // Special offers
            var specialOffers = await _unitOfWork.Items.Find(i => i.SpecialOffer != null);


            foreach (var item in specialOffers)
            {
                var itemOverview = new ItemOverviewViewModel()
                {
                    Name = item.Name,
                    Price = decimal.Round(item.Price, 2, MidpointRounding.AwayFromZero).ToString() + " zł",
                    ImageURL = PathModifier.MakeRelativePath(item.Images.FirstOrDefault()!.URL, _hostEnvironment.WebRootPath + "/wwwroot", '/')                    
                };

                homePageVM.SpecialOfferItems.Add(itemOverview);
            }

            // Top 10 items

            return View(homePageVM);
        }

        public async Task<IActionResult> Category(int id)
        {
            var rootCategories = await _unitOfWork.Categories.GetRootCategories();
            var homePageVM = new HomePageItemsViewModel();

            foreach (var category in rootCategories)
            {
                var categoryVM = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsVisible = category.IsVisible,
                    ChildCategories = category.ChildCategories
                };

                homePageVM.categories.Add(categoryVM);
            }

            var newItems = await _unitOfWork.Items.Find(i => i.IsNew == true);

            homePageVM.displayHome = false;

            var itemsByCategory = await _unitOfWork.Items.Find(i => (i.Category != null && i.Category.Id == id));

            foreach (var item in itemsByCategory)
            {
                var itemOverview = new ItemOverviewViewModel()
                {
                    Name = item.Name,
                    Price = decimal.Round(item.Price, 2, MidpointRounding.AwayFromZero).ToString() + " zł",
                    ImageURL = PathModifier.MakeRelativePath(item.Images.FirstOrDefault()!.URL, _hostEnvironment.WebRootPath + "/wwwroot", '/')
                };

                homePageVM.ItemsByCategory.Add(itemOverview);
            }


            return View("index", homePageVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}