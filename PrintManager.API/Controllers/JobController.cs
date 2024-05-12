using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PrintManager.Application.Contracts.Job;
using PrintManager.Application.Filters.Job;
using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Net;

namespace PrintManager.API.Controllers;

/// <summary>
/// Контроллер для управления заданиями печати.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly Random random = new Random();

    /// <summary>
    /// Создание задания печати
    /// </summary>
    [HttpPost]
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

        await SimulatePrinting();

        generatedJob.StatusId = IsPrintSuccess() ?
            (int)Logic.Enums.Status.Success :
            (int)Logic.Enums.Status.Failure;

        Job newJob = await jobService.CreateAsync(generatedJob);

        return CreatedAtAction(nameof(GetById), new { id = newJob.JobId }, newJob);
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
        return Ok(await jobService.GetByIdAsync(id));
    }

    [HttpPost("import-csv")]
    [ProducesResponseType(typeof(Job), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> ImportJobsFromCsv(
    [FromServices] IJobService jobService,
    [FromServices] ICSVService cSVService,
    [FromForm] IFormFileCollection file)
    {
        if (file.Count == 0)
        {
            return BadRequest("File is empty");
        }

        List<Job> data = cSVService.ReadCSV<CsvJobData>(file[0].OpenReadStream())
            .Where(ValidateRecord)
            .Select(CreateJobFromRecord)
            .ToList();

        //List<CsvJobData> data = cSVService.ReadCSV<CsvJobData>(file[0].OpenReadStream())
        //    .Where(jd =>
        //    jd.EmployeeId is not null &&
        //    jd.PrinterId is not null &&
        //    jd.PagesPrinted is not null &&
        //    jd.PrintJobName is not null)
        //    .ToList();

        //IReadOnlyList<Job> importedJobs = jobService.ImportJobsFromCsvAsync();

        return Ok(data);

            //return UnprocessableEntity(ex.Message);
    }

    private bool ValidateRecord(CsvJobData record)
    {
        ValidationContext context = new(record);

        return Validator.TryValidateObject(record, context, null, validateAllProperties: true);
    }

    private Job CreateJobFromRecord(CsvJobData record)
    {
        return new()
        {
            PrintJobName = record.PrintJobName,
            EmployeeId = record.EmployeeId.Value,
            PrinterId = record.PrinterId.Value,
            PagesPrinted = record.PagesPrinted.Value,
        };
    }

    private async Task SimulatePrinting()
    {
        int delayMilliseconds = random.Next(1000, 4001);

        await Task.Delay(delayMilliseconds);
    }

    private bool IsPrintSuccess()
    {
        int result = random.Next(1, 3);

        return result == (int)Logic.Enums.Status.Success;
    }
}
