using ApartmentManagement.Core.DTOs;
using ApartmentManagement.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ApartmentManagement.Web.ViewModels
{
    public class ApartmentCostPayViewModel
    {
        public PaymentDto PaymentDto { get; set; }

        [ValidateNever]
        public ApartmentCost ApartmentCost { get; set; }

    }
}
