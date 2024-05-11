using CsvHelper.Configuration.Attributes;
using PrintManager.Applpication.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Applpication.Contracts.Job
{
    public class CsvJobData
    {
        [Required(ErrorMessage = "PrintJobName is required.")]
        [Name("Наименование")]
        public required string PrintJobName { get; init; }

        [Required(ErrorMessage = "EmployeeId is required.")]
        [MinValue(1)]
        [Name("Сотрудник")]
        public required int EmployeeId { get; init; }

        [NonNegative]
        [Name("Порядковый номер")]
        public int? PrinterId { get; init; }

        [Required(ErrorMessage = "PagesPrinted is required.")]
        [MinValue(1)]
        [Name("Количество страниц")]
        public required int PagesPrinted { get; init; }
    }
}
