using ApartmentManagement.Core.Services;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApartmentManagement.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ApartmentController : Controller
    {
        private readonly IApartmentService _apartmentService;
        private readonly IUserService _userService;

        public ApartmentController(IApartmentService apartmentService, IUserService userService)
        {
            _apartmentService = apartmentService;
            _userService = userService;
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Apartment> apartmentList = await _apartmentService.GetAll();
            return View(apartmentList);
        }

        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                await _apartmentService.AddApartment(apartment);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var apartment = await _apartmentService.GetById(id);
            var aparmentUsers = await _userService.GetAllNonResidentUsers();

            UserApartmentViewModel userApartmentViewModel = new UserApartmentViewModel()
            {
                Apartment = apartment,
                AparmentUsers = aparmentUsers.Select(x => new SelectListItem()
                {
                    Text = x.Name + " " + x.LastName,
                    Value = x.Id.ToString()
                })
            };

            return View(userApartmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserApartmentViewModel userApartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                await _apartmentService.UpdateApartment(userApartmentViewModel.Apartment);
                return RedirectToAction(nameof(Index));
            }
            return View(userApartmentViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var apartment = await _apartmentService.GetById(id);
            return View(apartment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _apartmentService.RemoveApartment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
