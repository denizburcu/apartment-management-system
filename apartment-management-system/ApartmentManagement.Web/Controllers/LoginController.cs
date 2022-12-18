using ApartmentManagement.Core.Models;
using ApartmentManagement.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApartmentManagement.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Login", "Giriş yapılamadı");
                return View(userLoginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(userLoginViewModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("Login", "Kullanıcı bulunamadı");
                return View(userLoginViewModel);
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, userLoginViewModel.Password);
            if (!isPasswordCorrect)
            {
                ModelState.AddModelError("Login", "Şifre yanlış");
                return View(userLoginViewModel);
            }

            var identity = await AddIdentityClaimsForUser(user);
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private async Task<ClaimsIdentity> AddIdentityClaimsForUser(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            
            foreach (var role in roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            
            return identity;
        }
    }
}
