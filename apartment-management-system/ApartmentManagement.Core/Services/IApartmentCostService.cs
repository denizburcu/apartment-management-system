using ApartmentManagement.Core.DTOs;
using ApartmentManagement.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentManagement.Core.Services
{
    public interface IApartmentCostService
    {
        Task AddApartmentCost(ApartmentCost apartmentCost);
        Task<IEnumerable<ApartmentCost>> GetAllApartmentCostByMonth(Month month);
        Task<IEnumerable<ApartmentCost>> GetAllApartmentCostsByUser(string userId);
        Task<IEnumerable<ApartmentCost>> GetAllApartmentCostsByPaid(bool isPaid);
        Task<IActionResult> ExportApartmentCostByFileType(ExportFileType exportFileType);
        Task<IEnumerable<string>> GetAllEmailByUnpaidApartmentCosts();
        Task<bool> PayApartmentCost(PaymentDto paymentDto, int apartmentCostId);
        Task<ApartmentCost> GetById(int id);
    }
}
