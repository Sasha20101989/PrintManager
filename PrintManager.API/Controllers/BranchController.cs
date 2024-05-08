using Microsoft.AspNetCore.Mvc;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;

namespace PrintManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromServices] IBranchService branchService)
        {
            IReadOnlyList<Branch> branches = await branchService.GetAllAsync();

            return Ok(branches);
        }
    }
}
