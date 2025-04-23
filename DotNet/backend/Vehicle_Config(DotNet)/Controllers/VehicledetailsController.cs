using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Services;

namespace Vehicle_Config_DotNet_.Controllers
{
    [Route("api/vehicle-components")]
    [ApiController]
    public class VehicleDetailController : ControllerBase
    {
        private readonly IVehicleDetailService _vehicleDetailService;

        public VehicleDetailController(IVehicleDetailService vehicleDetailService)
        {
            _vehicleDetailService = vehicleDetailService;
        }

        // Get components by model ID
        [HttpGet("{modelId}")]
        public async Task<ActionResult<List<VehicleDetail>>> GetComponentsByModel(long modelId)
        {
            // Get the VehicleDetails with related data (including Component and Model)
            var components = await _vehicleDetailService.GetComponentsByModelIdAsync(modelId);

            if (components == null || components.Count == 0)
            {
                return NotFound("No components found for the given model ID.");
            }

            // Project the data into a simpler structure to avoid circular references
            var result = components.Select(vd => new
            {
                vd.ConfiId,
                vd.CompType,
                vd.IsConfigurable,
                // Include only required fields to avoid circular reference
                Comp = new { vd.Comp.CompId, vd.Comp.CompName },  // Adjust according to your actual component properties
                Model = new { vd.Model.Id, vd.Model.ModName } // Adjust according to your actual model properties
            }).ToList();

            return Ok(result);
        }
    }
}
