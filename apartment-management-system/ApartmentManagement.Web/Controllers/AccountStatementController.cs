using ApartmentManagement.Core.Services;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApartmentManagement.Web.Controllers
{
    public class AccountStatementController : Controller
    {
        private readonly IApartmentCostService _apartmentCostService;

        public AccountStatementController(IApartmentCostService apartmentCostService)
        {
            _apartmentCostService = apartmentCostService;
        }
        
        public async Task<IActionResult> Index()
        {
            var apartmentCosts = await _apartmentCostService.GetAllApartmentCostsByPaid(true);
            var aparmentCostExportViewModel = new ApartmentCostExportViewModel()
            {
                ApartmentCosts = apartmentCosts,
                ExportFileTypes = GetFileTypes(),
                SelectedFileType = ExportFileType.EXCEL
            };

            return View(aparmentCostExportViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ApartmentCostExportViewModel model)
        {
            return await _apartmentCostService.ExportApartmentCostByFileType(model.SelectedFileType);
        }

        private IEnumerable<SelectListItem> GetFileTypes()
        {
            return Enum.GetValues(typeof(ExportFileType))
                            .Cast<ExportFileType>()
                            .ToList().Select(x => new SelectListItem()
                            {
                                Text = x.ToString(),
                                Value = ((int)x).ToString()
                            });
        }
    }
}
