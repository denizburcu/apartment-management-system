using Fingers10.ExcelExport.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ApartmentManagement.Core.Models
{
    public class ApartmentCost : BaseEntity
    {
        [IncludeInReport(Order = 1)]
        public CostType CostType { get; set; }

        [IncludeInReport(Order = 2)]
        public int Amount { get; set; }

        [IncludeInReport(Order = 3)]
        public bool IsPaid { get; set; }

        [IncludeInReport(Order = 4)]
        public Month Month { get; set; }

        [IncludeInReport(Order = 5)]
        public int ApartmentId { get; set; }
        [ValidateNever]
        public Apartment Apartment { get; set; }
    }
    public enum Month
    {
        JANUARY = 1,
        FEBRUARY = 2,
        MARCH = 3,
        APRIL = 4,
        MAY= 5,
        JUNE = 6,
        JULY = 7,
        AGUST = 8,
        SEPTEMBER = 9,
        OCTOBER = 10,
        NOVEMBER = 11,
        DECEMBER = 12,
    }

    public enum CostType
    {
        ELECTRICITY,
        WATER,
        GAS,
        DUES
    }

    public enum ExportFileType
    {
        PDF = 1,
        EXCEL = 2
    }
}
