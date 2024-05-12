using CsvHelper.Configuration.Attributes;
using PrintManager.Application.Attributes;
using PrintManager.Application.Properties;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Application.Contracts.Job
{
    public class CsvJobData
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredPrintJobNameErrorMessage")]
        [Name("Наименование")]
        public string? PrintJobName { get; init; }

        [Required(ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredEmployeeIdErrorMessage")]
        [MinValue(1)]
        [Name("Сотрудник")]
        public int? EmployeeId { get; init; }

        [NonNegative]
        [Name("Порядковый номер")]
        public int? PrinterId { get; init; }

        [Required(ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredPagesPrintedErrorMessage")]
        [MinValue(1)]
        [Name("Количество страниц")]
        public int? PagesPrinted { get; init; }
    }
}
