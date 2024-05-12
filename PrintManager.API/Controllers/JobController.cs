using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Contracts.Job;
using PrintManager.Application.Filters.Job;
using PrintManager.Application.Filters.Printer;
using PrintManager.Application.Filters.Employee;
using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;

using System.Net;
using Microsoft.Extensions.Localization;
using PrintManager.Application.DTOs;

namespace PrintManager.API.Controllers;

/// <summary>
/// Контроллер для управления заданиями печати.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    /// <summary>
    /// Создание задания печати
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Job), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(Printer_ValidatePrinterIdFilterAttribute))]
    [ServiceFilter(typeof(Employee_ValidateEmployeeIdFilterAttribute))]
    public async Task<IActionResult> Create(
        [FromServices] IJobService jobService,
        [FromBody] CreateJobRequest createJobRequest)
    {
        Job generatedJob = await jobService.GenerateAsync(
            createJobRequest.PrintJobName,
            createJobRequest.EmployeeId,
            createJobRequest.PagesPrinted,
            createJobRequest.PrinterId == 0 ? null : createJobRequest.PrinterId
        );

        await jobService.SimulatePrintingAsync(generatedJob);

        JobDTO jobDTO = JobDTO.Create(await jobService.CreateJobAsync(generatedJob));

        return CreatedAtAction(nameof(GetById), new { id = jobDTO.JobId }, jobDTO);
    }

    /// <summary>
    /// Получение сведений о задании печати по параметру, позволяющему его уникально идентифицировать
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Job), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(Job_ValidateJobIdFilterAttribute))]
    public async Task<IActionResult> GetById(
    [FromServices] IJobService jobService,
    int id)
    {
        return Ok(JobDTO.Create(await jobService.GetByIdAsync(id)));
    }

    /// <summary>
    /// Импорт заданий печати и файла с расширением (.csv)
    /// </summary>
    [HttpPost("import-csv")]
    [ProducesResponseType(typeof(Job), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ServiceFilter(typeof(Job_ValidateFileCountFilterAttribute))]
    public async Task<IActionResult> ImportJobsFromCsv(
        [FromServices] IStringLocalizer<JobController> localizer,
    [FromServices] IJobService jobService,
    [FromForm] IFormFileCollection file)
    {
        IReadOnlyList<CsvJobData> data = jobService.ImportJobsFromCsv(file[0].OpenReadStream());

        await Task.WhenAll(data.Select(async r => await jobService.CreateJobFromRecordAsync(r)));

        return Ok(localizer["SuccessMessage", data.Count].Value);
    }
}
