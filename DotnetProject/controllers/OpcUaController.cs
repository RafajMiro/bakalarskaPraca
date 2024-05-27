// OpcUaContoller.cs
using Microsoft.AspNetCore.Mvc;
using OpcUaApi.Services;
using OpcUaApi.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OpcUaController : ControllerBase
{
    private readonly OpcUaService _opcUaService;

    public OpcUaController(OpcUaService opcUaService)
    {
        _opcUaService = opcUaService;
    }

    [HttpPost("connect")]
    public async Task<IActionResult> Connect([FromBody] EndpointRequest request)
    {
        if (string.IsNullOrEmpty(request?.Endpoint))
        {
            return BadRequest(new { message = "Endpoint URL is required." });
        }

        var message = await _opcUaService.Connect(request.Endpoint);
        return Ok(new { message });
    }

    [HttpGet("browse")]
    public async Task<IActionResult> Browse([FromQuery] string endpoint, [FromQuery] string nodeId = "")
    {
        try
        {
            var nodeDataList = await _opcUaService.BrowseNodes(endpoint, nodeId);
            return Ok(nodeDataList);
        }
        catch (System.Exception ex)
        {
            return BadRequest($"Failed to browse: {ex.Message}");
        }
    }
}