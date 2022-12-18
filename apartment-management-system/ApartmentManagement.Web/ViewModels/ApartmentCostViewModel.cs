using ApartmentManagement.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApartmentManagement.Web.ViewModels
{
    public class ApartmentCostViewModel
    {
        public ApartmentCost ApartmentCost { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Apartments { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Types { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Months { get; set; }
    }
}
