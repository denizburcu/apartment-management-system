using ApartmentManagement.Core.Contracts;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApartmentManagement.Web.Controllers
{
    public class ApartmentCostController : Controller
    {
        private readonly IApartmentCostService _apartmentCostService;
        private readonly IApartmentService _apartmentService;

        public ApartmentCostController(IApartmentCostService apartmentCostService, IApartmentService apartmentService)
        {
            _apartmentCostService = apartmentCostService;
            _apartmentService = apartmentService;
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> Index()
        {
            var month = Month.OCTOBER;
            var apartmentCosts = new object();

            if (TempData["SelectedMonth"] != null)
                month = (Month)TempData["SelectedMonth"];

            if (User.IsInRole("Admin"))
            {
                apartmentCosts = await _apartmentCostService.GetAllApartmentCostByMonth(month);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                apartmentCosts = await _apartmentCostService.GetAllApartmentCostsByUser(claim.Value);
            }

            var aparmentCostMonthView = new ApartmentCostMonthViewModel()
            {
                ApartmentCosts = (IEnumerable<ApartmentCost>)apartmentCosts,
                Months = GetMonths(),
                SelectedMonth = month
            };

            return View(aparmentCostMonthView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ApartmentCostMonthViewModel model)
        {
            TempData["SelectedMonth"] = model.SelectedMonth;
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var aparments = await _apartmentService.GetAll();

            var apartmentCostViewModel = new ApartmentCostViewModel()
            {
                Apartments = aparments.Select(a => new SelectListItem()
                {
                    Text = "Blok No: " + a.BlockNumber.ToString() + "Daire No: " + a.ApartmentNumber.ToString(),
                    Value = a.Id.ToString()
                }),
                Types = GetCostTypes(),
                Months = GetMonths()
            };

            return View(apartmentCostViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApartmentCostViewModel apartmentCostViewModel)
        {
            if (ModelState.IsValid)
            {
                await _apartmentCostService.AddApartmentCost(apartmentCostViewModel.ApartmentCost);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Pay(int id)
        {
            var apartmentCost = await _apartmentCostService.GetById(id);
            var apartmentCostPayViewModel = new ApartmentCostPayViewModel()
            {
                ApartmentCost = apartmentCost
            };
            return View(apartmentCostPayViewModel);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(ApartmentCostPayViewModel apartmentCostPayViewModel)
        {
            if (ModelState.IsValid)
            {
                apartmentCostPayViewModel.PaymentDto.PaidAmount = apartmentCostPayViewModel.ApartmentCost.Amount;
                var result = await _apartmentCostService.PayApartmentCost(apartmentCostPayViewModel.PaymentDto, apartmentCostPayViewModel.ApartmentCost.Id);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError("Payment", "Ödeme yapılamadı");
                    return View(apartmentCostPayViewModel);
                }
            }
            return View();
        }

        private IEnumerable<SelectListItem> GetMonths()
        {
            return Enum.GetValues(typeof(Month))
                            .Cast<Month>()
                            .ToList().Select(x => new SelectListItem()
                            {
                                Text = x.ToString(),
                                Value = ((int)x).ToString()
                            });
        }

        private IEnumerable<SelectListItem> GetCostTypes()
        {
            return Enum.GetValues(typeof(CostType))
                            .Cast<CostType>()
                            .ToList().Select(x => new SelectListItem()
                            {
                                Text = x.ToString(),
                                Value = ((int)x).ToString()
                            });
        }

    }
}
