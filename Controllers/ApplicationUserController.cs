using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Persistence.Repositories.WorkUnit;
using WebStore.ViewModels.ApplicationUserViewModels;
using IdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace WebStore.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private UserAccountManagementViewModel accountManagment;

        public ApplicationUserController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            accountManagment = new UserAccountManagementViewModel();
        }

        // ----------------------------------------------------

        public async Task<IActionResult> AccountManagment()
        {

            var user = await _userManager.GetUserAsync(User);

            accountManagment.EditEmail.Email = user.Email;

            accountManagment.EditShippingAddress.Name = user.FirstName;
            accountManagment.EditShippingAddress.Surname = user.LastName;
            accountManagment.EditShippingAddress.Phone = user.PhoneNumber;
            accountManagment.EditShippingAddress.PostalCode = user.PostalCode;
            accountManagment.EditShippingAddress.City = user.City;
            accountManagment.EditShippingAddress.Street = user.Street;
            accountManagment.EditShippingAddress.BuildingNumber = user.BuildingNumber;
            accountManagment.EditShippingAddress.ApartmentNumber = user.ApartmentNumber;

            return View("AccountManagment", accountManagment);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmail(UserAccountManagementViewModel userAccountManagementViewModel)
        {
            
                var user = await _userManager.GetUserAsync(User);

                user.Email = userAccountManagementViewModel.EditEmail.Email;

                var result = await _userManager.UpdateAsync(user);

                return View("AccountManagment");

        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(UserAccountManagementViewModel userAccountManagementViewModel)
        {

                var user = await _userManager.GetUserAsync(User);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, userAccountManagementViewModel.EditPassword.NewPassword);

                return View("AccountManagment");
            
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(UserAccountManagementViewModel userAccountManagementViewModel)
        {

                var user = await _userManager.GetUserAsync(User);

                user.FirstName = userAccountManagementViewModel.EditShippingAddress.Name;
                user.LastName = userAccountManagementViewModel.EditShippingAddress.Surname;
                user.PhoneNumber = userAccountManagementViewModel.EditShippingAddress.Phone;
                user.PostalCode = userAccountManagementViewModel.EditShippingAddress.PostalCode;
                user.City = userAccountManagementViewModel.EditShippingAddress.City;
                user.Street = userAccountManagementViewModel.EditShippingAddress.Street;
                user.BuildingNumber = userAccountManagementViewModel.EditShippingAddress.BuildingNumber;
                user.ApartmentNumber = userAccountManagementViewModel.EditShippingAddress.ApartmentNumber;

                await _userManager.UpdateAsync(user);

                return View("AccountManagment");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.DeleteAsync(user);

            if(result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            return View("AccountManagment");
        }

        // ----------------------------------------------------

        public async Task<IActionResult> LoginOrRegister()
        {
            var loginOrRegister = new UserLoginOrRegisterViewModel();

            return View(loginOrRegister);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel login)
        {
            var returnURL = Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded) { return LocalRedirect(returnURL); }
            }

            ViewData["LoginFailMsg"] = "Nieprawidłowy email lub hasło";
            return View("LoginOrRegister");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel register)
        {
            var returnURL = Url.Content("~/");

            if (ModelState.IsValid)
            {
                if (!await _roleManager.RoleExistsAsync("Regular User"))
                {
                    var regularUserRole = new IdentityRole() { Name = "Regular User" };
                    await _roleManager.CreateAsync(regularUserRole);
                }
                
                var user = new ApplicationUser() 
                { 
                    Email = register.Email, 
                    UserName = register.Email 
                };

                var result = await _userManager.CreateAsync(user, register.Password);
                await _userManager.AddToRoleAsync(user, "Regular User");
                
                if (result.Succeeded) 
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnURL);
                }

                if (result.Errors.FirstOrDefault(e => e.Code == "DuplicateUserName") != null)
                {
                    ViewData["RegisterFailMsg"] = "Ten email jest już w użyciu";
                }
                else
                {
                    ViewData["RegisterFailMsg"] = "Rejestracja nie powiodła się";
                }
            }

            else
            {
                ViewData["RegisterFailMsg"] = "Rejestracja nie powiodła się";
            }

            return View("LoginOrRegister");
        }
    }
}
