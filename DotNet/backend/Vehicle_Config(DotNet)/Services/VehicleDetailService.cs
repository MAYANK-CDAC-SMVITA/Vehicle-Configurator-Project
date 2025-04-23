using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;

namespace Vehicle_Config_DotNet_.Services
{
    public class VehicleDetailService : IVehicleDetailService
    {
        private readonly ProjectContext _context;

        public VehicleDetailService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleDetail>> GetComponentsByModelIdAsync(long modelId)
        {
            // Fetch vehicle details including related components and models
            return await _context.VehicleDetails
                .Include(vd => vd.Comp)  // Load related Component details
                .Include(vd => vd.Model) // Load related Model details
                .Where(vd => vd.ModelId == modelId)
                .ToListAsync();
        }
    }
}
