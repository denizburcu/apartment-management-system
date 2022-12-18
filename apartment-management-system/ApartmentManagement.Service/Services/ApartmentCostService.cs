using ApartmentManagement.Core.Services;
using ApartmentManagement.Core.DTOs;
using ApartmentManagement.Core.IUnitOfWorks;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Repositories;
using Fingers10.ExcelExport.ActionResults;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using Microsoft.AspNetCore.Mvc;


namespace ApartmentManagement.Service.Services
{
    public class ApartmentCostService : IApartmentCostService
    {
        private readonly CreditCardClientService _creditCardClientService;
        private readonly IApartmentCostRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ApartmentCostService(IApartmentCostRepository repository, IUnitOfWork unitOfWork, CreditCardClientService creditCardClientService)
        {
            _creditCardClientService = creditCardClientService;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddApartmentCost(ApartmentCost apartmentCost)
        {
            await _repository.AddAsync(apartmentCost);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IActionResult> ExportApartmentCostByFileType(ExportFileType exportFileType)
        {
            var allPaidCosts = await _repository.GetAllPaidOrderByDescendingAsync();
            if (exportFileType == ExportFileType.EXCEL)
                return GenerateExcel(allPaidCosts);
            else
                return GeneratePdf(allPaidCosts);
        }

        public async Task<IEnumerable<ApartmentCost>> GetAllApartmentCostByMonth(Month month)
        {
            return await _repository.GetAllNotPaidCostsByMonthIncludeApartmentAsync(month);
        }

        public async Task<IEnumerable<ApartmentCost>> GetAllApartmentCostsByPaid(bool isPaid)
        {
            return await _repository.GetAllByIsPaidIncludeApartmentAsync(isPaid);
        }

        public async Task<IEnumerable<ApartmentCost>> GetAllApartmentCostsByUser(string userId)
        {
            return await _repository.GetAllByUserId(userId);
        }

        public async Task<IEnumerable<string>> GetAllEmailByUnpaidApartmentCosts()
        {
            return await _repository.GetAllEmailsByNotPaidApartmentCostsAsync();
        }

        public async Task<bool> PayApartmentCost(PaymentDto paymentDto, int apartmentCostId)
        {
            var paymentResult = await _creditCardClientService.MakePayment(paymentDto);
            var apartmentCost = await GetById(apartmentCostId);
            if (paymentResult)
            {
                apartmentCost.IsPaid = true;
                _repository.Update(apartmentCost);
                await _unitOfWork.CommitAsync();
            }
            return apartmentCost.IsPaid;
        }

        public async Task<ApartmentCost> GetById(int id)
        {
            return await _repository.GetByIdIncludeAparmentAsync(id);
        }

        private FileStreamResult GeneratePdf(IEnumerable<ApartmentCost> costs)
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.Pages.Add();

            PdfGraphics graphics = page.Graphics;

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            graphics.DrawString("Fatura Tipi--Fiyat--Durum--Ay--Apartment No", font, PdfBrushes.Black, new PointF(0, 0));
            int i = 25;
            foreach (ApartmentCost cost in costs)
            {
                graphics.DrawString(cost.CostType + "--" + cost.Amount + "TL" + "--" + cost.IsPaid + "--" + cost.Month + "--" + cost.Apartment.ApartmentNumber, font, PdfBrushes.Black, new PointF(0, i));
                i += 25;
            }

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = "paidcost.pdf";
            return fileStreamResult;
        }

        private ExcelResult<ApartmentCost> GenerateExcel(IEnumerable<ApartmentCost> costs)
        {
            return new ExcelResult<ApartmentCost>(costs, "sheet1", "PaidCostReport");
        }

    }
}
