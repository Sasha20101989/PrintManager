using CsvHelper.Configuration.Attributes;
using PrintManager.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Application.Contracts.Job
{
    public class CsvJobData
    {
        [Required(ErrorMessage = "PrintJobName is required.")]
        [Name("Наименование")]
        public string? PrintJobName { get; init; }

        [Required(ErrorMessage = "EmployeeId is required.")]
        [MinValue(1)]
        [Name("Сотрудник")]
        public int? EmployeeId { get; init; }

        [NonNegative]
        [Name("Порядковый номер")]
        public int? PrinterId { get; init; }

        [Required(ErrorMessage = "PagesPrinted is required.")]
        [MinValue(1)]
        [Name("Количество страниц")]
        public int? PagesPrinted { get; init; }
    }
}
