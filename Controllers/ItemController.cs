using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Other;
using WebStore.Persistence.Repositories.WorkUnit;
using WebStore.ViewModels.ItemViewModels;

namespace WebStore.Controllers
{
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            //zwrocic liste pewnej ilosci itemow?
            //var items = 

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _unitOfWork.Items.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            var itemVM = new ItemDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = decimal.Round(item.Price, 2, MidpointRounding.AwayFromZero).ToString() + " zł",
                StockQuantity = item.StockQuantity,
                CategoryId = item.Category.Id
            };

            foreach (ItemImage image in item.Images)
            {
                itemVM.ImageURLs.Add("../" + PathModifier.MakeRelativePath(image.URL, _hostEnvironment.WebRootPath + "/wwwroot", '/'));
            }

            return View(itemVM);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Item item)
        {
            string categoryId = Request.Form["Category"];
            if (categoryId != null && !categoryId.IsEmpty())
            {
                item.Category = await _unitOfWork.Categories.Get(Int32.Parse(categoryId));
            }

            /*
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();
            */

            if (ModelState.IsValid)
            {
                string itemImagesFolderPath = _hostEnvironment.WebRootPath + "\\images\\item_images\\";

                foreach (IFormFile imageFile in item.ImageFiles)
                {
                    string uniqueImageName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string imageURL = Path.Combine(itemImagesFolderPath, uniqueImageName);

                    var itemImage = new ItemImage()
                    {
                        URL = imageURL,
                        Item = item
                    };

                    item.Images.Add(itemImage);

                    using (var fileStream = new FileStream(imageURL, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                }

                await _unitOfWork.Items.Add(item);
                await _unitOfWork.Complete();
            }

            return View();
            //return RedirectToAction("Details", item.Id);
        }

        public async Task<IActionResult> Edit(int id)       
        {
            var item = await _unitOfWork.Items.Get(id);

            if (item == null)
            {
                return BadRequest();
            }

            foreach (ItemImage image in item.Images)
            {
                image.URL = PathModifier.MakeRelativePath(image.URL, _hostEnvironment.WebRootPath + "/wwwroot", '/');
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Item item)
        {
            string categoryId = Request.Form["Category"];
            if (categoryId != null && !categoryId.IsEmpty())
            {
                item.Category = await _unitOfWork.Categories.Get(Int32.Parse(categoryId));
            }

            ModelState.Remove("ImageFiles");

            if (ModelState.IsValid)
            {
                var itemToUpdate = await _unitOfWork.Items.Get(item.Id);

                itemToUpdate.Name = item.Name;
                itemToUpdate.Description = item.Description;
                itemToUpdate.Price = item.Price;
                itemToUpdate.StockQuantity = item.StockQuantity;
                itemToUpdate.IsVisible = item.IsVisible;
                itemToUpdate.Category = item.Category;

                string itemImagesFolderPath = _hostEnvironment.WebRootPath + "\\images\\item_images\\";

                foreach (IFormFile imageFile in item.ImageFiles)
                {
                    string uniqueImageName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string imageURL = Path.Combine(itemImagesFolderPath, uniqueImageName);

                    var itemImage = new ItemImage()
                    {
                        URL = imageURL,
                        Item = itemToUpdate
                    };

                    itemToUpdate.Images.Add(itemImage);

                    using (var fileStream = new FileStream(imageURL, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                }

                await _unitOfWork.Complete();

                return NoContent();
            }

            return NoContent();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.Items.Get(id);

            if(item == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Items.Delete(item);
            await _unitOfWork.Complete();

            return LocalRedirect(Url.Content("~/"));
        }
    }
}
