using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Services;
namespace Vehicle_Config_DotNet_.Controllers
{
    [ApiController]
    [Route("api/segments/")]
    public class SegmentController : ControllerBase
    {
        private readonly ISegmentService iref;

        public SegmentController(ISegmentService iref)
        {
            this.iref = iref;
        }

        [HttpPost]
        public async Task<IActionResult> AddSegment(Segment seg)
        {
            await iref.add(seg);
            return CreatedAtAction("AddSegment", new { id = seg.Id }, seg);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Segment>>> GetSegments()
        {
            return await iref.getAllSegments();
        }
    }
}
