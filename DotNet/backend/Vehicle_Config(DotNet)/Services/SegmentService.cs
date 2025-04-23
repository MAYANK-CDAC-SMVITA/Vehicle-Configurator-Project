using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vehicle_Config_DotNet_.Services
{
    public class SegmentService : ISegmentService
    {
        private readonly ProjectContext context;

        public SegmentService(ProjectContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<Segment>> add(Segment seg)
        {
            context.Segments.Add(seg);
            await context.SaveChangesAsync();
            return seg;
        }

        public async Task<ActionResult<IEnumerable<Segment>>> getAllSegments()
        {
            return await context.Segments.ToListAsync();
        }
    }
}
