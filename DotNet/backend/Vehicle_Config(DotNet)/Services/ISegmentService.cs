using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle_Config_DotNet_.Models;

namespace Vehicle_Config_DotNet_.Services
{
    public interface ISegmentService
    {
        Task<ActionResult<Segment>> add(Segment segment);

        Task<ActionResult<IEnumerable<Segment>>> getAllSegments();
    }
}
