using Microsoft.EntityFrameworkCore;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle_Config_DotNet_.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly ProjectContext context;

        // Constructor to inject ProjectContext (EF DbContext)
        public ManufacturerService(ProjectContext context)
        {
            this.context = context;
        }

        // Method to fetch manufacturers by segmentId
        public async Task<IEnumerable<Manufacturer>> GetManufacturersBySegmentId(long segmentId)
        {
            // Fetch the manufacturers associated with the provided segmentId
            var manufacturers = await context.Manufacturers
                .Where(m => m.SegId == segmentId) // Filter manufacturers by segmentId
                .ToListAsync(); // Execute the query and return the list

            return manufacturers;  // Return the list of manufacturers
        }
    }
}
