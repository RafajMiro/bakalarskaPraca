// AasController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AasApi.Services;
using AasApi.Models;

namespace AasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AasController : ControllerBase
    {
        private readonly IAasService _aasService;

        public AasController(IAasService aasService)
        {
            _aasService = aasService;
        }

        [HttpGet("shells")]
        public async Task<IActionResult> GetAllAasData()
        {
            Console.WriteLine("fddd");
            var aasListResponse = await _aasService.GetAllAasDataAsync();
            if (aasListResponse == null)
            {
                return NotFound();
            }

            return Ok(aasListResponse);
        }

        [HttpGet("shells/{shellId}")]
        public async Task<IActionResult> GetAasData(string shellId)
        {
            var aasData = await _aasService.GetAasDataAsync(shellId);
            if (aasData == null)
            {
                return NotFound();
            }

            return Ok(aasData);
        }

        [HttpGet("submodels/{submodelId}")]
        public async Task<IActionResult> GetSubmodelData(string submodelId)
        {
            var submodelData = await _aasService.GetSubmodelDataAsync(submodelId);
            if (submodelData == null)
            {
                return NotFound();
            }

            return Ok(submodelData);
        }

        [HttpPut("submodels/{submodelId}")]
        public async Task<IActionResult> UpdateSubmodelData(string submodelId, [FromBody] SubmodelData submodelData)
        {
            if (submodelData.Id == null)
            {
                return BadRequest("Submodel data ID is null");
            }
            string encodedSubmodelId = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(submodelData.Id));

            if (submodelId != encodedSubmodelId)
            {
                return BadRequest("Submodel ID mismatch");
            }

            var result = await _aasService.UpdateSubmodelDataAsync(submodelId, submodelData);
            if (!result)
            {
                return StatusCode(500, "An error occurred while updating the submodel");
            }

            return NoContent();
        }
    }
}
