using ApartmentManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordGenerator;

namespace ApartmentManagement.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> Index()
        {
            var userList = await _userManager.Users.ToListAsync();
            return View(userList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            TempData["password"] = null;
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var password = RandomPassword();
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);

                return View(user);
            }

            await _userManager.AddToRoleAsync(user, "USER");
            TempData["password"] = password;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User userView)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userView.Id);
                user.Name = userView.Name;
                user.LastName = userView.LastName;
                user.Email = userView.Email;
                user.IdentityNumber = userView.IdentityNumber;
                user.PlateNumber = userView.PlateNumber;
                user.UserName = userView.UserName;
                user.PhoneNumber = userView.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(userView);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id); ;
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        private string RandomPassword()
        {
            var pw = new Password();
            return pw.Next();
        }
    }
}
