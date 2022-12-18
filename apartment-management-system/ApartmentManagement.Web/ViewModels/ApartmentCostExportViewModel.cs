using ApartmentManagement.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApartmentManagement.Web.ViewModels
{
    public class ApartmentCostExportViewModel
    {
        public IEnumerable<ApartmentCost> ApartmentCosts;

        public IEnumerable<SelectListItem> ExportFileTypes;
        [ValidateNever]
        public ExportFileType SelectedFileType { get; set; }
    }
}
