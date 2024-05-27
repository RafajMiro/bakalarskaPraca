// WorkbenchController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AasApi.Services;
using AasApi.Models;
using System.Collections.Generic;

namespace AasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkbenchController : ControllerBase
    {
        private readonly IWorkbenchService _workbenchService;

        public WorkbenchController(IWorkbenchService workbenchService)
        {
            _workbenchService = workbenchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkbenches()
        {
            var workbenches = await _workbenchService.GetAllWorkbenchesAsync();
            return Ok(workbenches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkbench(string id)
        {
            var workbench = await _workbenchService.GetWorkbenchAsync(id);
            if (workbench == null)
            {
                return NotFound();
            }
            return Ok(workbench);
        }

        [HttpPost]
        public async Task<IActionResult> SaveWorkbench([FromBody] Workbench workbench)
        {
            var result = await _workbenchService.SaveWorkbenchAsync(workbench);
            if (!result)
            {
                return StatusCode(500, "An error occurred while saving the workbench");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkbench(string id)
        {
            var result = await _workbenchService.DeleteWorkbenchAsync(id);
            if (!result)
            {
                return StatusCode(500, "An error occurred while deleting the workbench");
            }
            return NoContent();
        }
    }
}
