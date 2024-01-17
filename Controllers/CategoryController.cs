using Microsoft.AspNetCore.Mvc;
using WebStore.Persistence.Repositories.WorkUnit;
using WebStore.ViewModels.CategoryViewModels;

namespace WebStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetRootCategories()
        {
            var rootCategories = await _unitOfWork.Categories.GetRootCategories();
            var rootCategoriesVM = new List<CategoryBrowserViewModel>();   

            foreach (var category in rootCategories)
            {                
                var categoryVM = new CategoryBrowserViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = 0,
                    HasChildren = category.ChildCategories.Any()
                };

                rootCategoriesVM.Add(categoryVM);
            }

            return Ok(rootCategoriesVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetParentOf(int id)
        {
            var childCategory = await _unitOfWork.Categories.Get(id);

            if (childCategory == null) 
            {
                return BadRequest();
            }

            var parentCategory = childCategory.ParentCategory;

            if (parentCategory == null) 
            {
                return BadRequest();
            }

            var parentCategoryVM = new CategoryBrowserViewModel
            {
                Id = parentCategory.Id,
                Name = parentCategory.Name,
                ParentId = (parentCategory.ParentCategory == null) ? 0 : parentCategory.ParentCategory.Id,
                HasChildren = parentCategory.ChildCategories.Any()
            };

            return Ok(parentCategoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetChildrenOf(int id)
        {
            var parentCategory = await _unitOfWork.Categories.Get(id);
            
            if (parentCategory == null)
            {
                return BadRequest();
            }

            var childCategories = parentCategory.ChildCategories;

            if (childCategories == null || childCategories.Count == 0)
            {
                return BadRequest();
            }

            var childCategoriesVM = new List<CategoryBrowserViewModel>();

            foreach (var category in childCategories)
            {
                var categoryVM = new CategoryBrowserViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentCategory.Id,
                    HasChildren = category.ChildCategories.Any()
                };

                childCategoriesVM.Add(categoryVM);
            }

            return Ok(childCategoriesVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _unitOfWork.Categories.Get(id);

            if (category == null)
            {
                return BadRequest();
            }

            var categoryVM = new CategoryBrowserViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ParentId= (category.ParentCategory == null) ? 0 : category.ParentCategory.Id,
                HasChildren = category.ChildCategories.Any()
            };

            return Ok(categoryVM);
        }
    }
}
