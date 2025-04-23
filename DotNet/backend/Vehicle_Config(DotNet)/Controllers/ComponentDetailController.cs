using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle_Config_DotNet_.Services;

namespace Vehicle_Config_DotNet_.Controllers
{
    [Route("api/components")]
    [ApiController]
    public class ComponentDetailController : ControllerBase
    {
        private readonly ComponentDetailService _componentDetailService;

        public ComponentDetailController(ComponentDetailService componentDetailService)
        {
            _componentDetailService = componentDetailService;
        }

        [HttpGet("{modelId}")]
        public async Task<IActionResult> GetComponentDetailsByModelId(long modelId)
        {
            //try
            //{
                var response = await _componentDetailService.GetComponentDetailsByModelIdAsync(modelId);

            return Ok(response);
            //}
            //catch (Exception ex)
            //{
                //return StatusCode(500, new { message = "Internal Server Error", error = ex });
            //}
        }
    }
}
