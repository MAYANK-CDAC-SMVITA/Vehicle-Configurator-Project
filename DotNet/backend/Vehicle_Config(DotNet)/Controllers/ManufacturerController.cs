using Microsoft.AspNetCore.Mvc;
using Vehicle_Config_DotNet_.Services;
using Vehicle_Config_DotNet_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vehicle_Config_DotNet_.Controllers
{
    [ApiController]
    [Route("api/manufacturers")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        // Constructor to inject IManufacturerService
        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        // HTTP GET endpoint to get manufacturers by segmentId
        [HttpGet("{segmentId}")]
        public async Task<IActionResult> GetManufacturersBySegmentId(long segmentId)
        {
            // Call the service method to get manufacturers by segmentId
            var manufacturers = await _manufacturerService.GetManufacturersBySegmentId(segmentId);

            if (manufacturers == null || !manufacturers.Any())  // Check if no manufacturers found
            {
                return NotFound($"No manufacturers found for segmentId: {segmentId}");  // Return 404 if not found
            }

            return Ok(manufacturers);  // Return manufacturers as a response (200 OK)
        }
    }
}
