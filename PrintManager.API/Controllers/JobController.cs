using Microsoft.AspNetCore.Mvc;
using PrintManager.Applpication.Contracts.Job;
using PrintManager.Applpication.Filters.Job;
using PrintManager.Applpication.Interfaces;
using PrintManager.Applpication.Services;
using PrintManager.Logic.Models;
using System.Net;
using System.Net.Security;

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
        if (file == null)
        {
            return BadRequest("File is empty");
        }

        List<CsvJobData> res = cSVService.ReadCSV<CsvJobData>(file[0].OpenReadStream()).ToList();

            //IReadOnlyList<Job> importedJobs = jobService.ImportJobsFromCsvAsync();

            return Ok(res);

            //return UnprocessableEntity(ex.Message);
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
