using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Services;

namespace Vehicle_Config_DotNet_    .Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly ILogger<ModelController> _logger;

        public ModelController(IModelService modelService, ILogger<ModelController> logger)
        {
            _modelService = modelService;
            _logger = logger;
        }

        [HttpGet("{segmentId}/{manufacturerId}")]
        public IActionResult GetModels(long segmentId, long manufacturerId)
        {
            _logger.LogInformation("Getting models for segmentId: {SegmentId}, manufacturerId: {ManufacturerId}", segmentId, manufacturerId);
            var models = _modelService.GetModelsBySegmentAndManufacturer(segmentId, manufacturerId);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelById(long id)
        {
            _logger.LogInformation("Getting model by id: {Id}", id);
            var result = await _modelService.GetModelByIdAsync(id);

            if (result == null)
            {
                _logger.LogWarning("Model with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(result);
        }
    }
}
