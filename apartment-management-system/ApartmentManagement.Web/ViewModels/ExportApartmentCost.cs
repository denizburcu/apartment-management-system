using Fingers10.ExcelExport.Attributes;

namespace ApartmentManagement.Core.Models.ViewModels
{
    public class ExportApartmentCost
    {
        [IncludeInReport()]
        public CostType Type { get; set; }
        [IncludeInReport()]
        public string ApartmentNumber { get; set; } = null!;
        [IncludeInReport()]
        public double Amount { get; set; }

    }
}
