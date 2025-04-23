using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;
namespace Vehicle_Config_DotNet_.Services
{ 
    public class ModelService : IModelService
    {
        private readonly ProjectContext context;

        public ModelService(ProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<object> GetModelsBySegmentAndManufacturer(long segmentId, long manufacturerId)
        {
            var result = context.Models
                .Include(m => m.Seg)
                .Include(m => m.Manu)
                .Where(m => m.Seg.Id == segmentId && m.Manu.Id == manufacturerId)
                .Select(m => new
                {
                    m.Id,
                    m.ImagePath,
                    Manufacturer = new
                    {
                        m.Manu.Id,
                        Name = m.Manu.ManuName
                    },
                    m.MinQty,
                    m.ModName,
                    m.Price,
                    m.SafetyRating,
                    Segment = new
                    {
                        m.Seg.Id,
                        Name = m.Seg.SegName
                    }
                })
                .ToList();

            return result;
        }

        public async Task<object> GetModelByIdAsync(long id)
        {
            var model = await context.Models
                .Include(m => m.Manu)
                .Include(m => m.Seg)
                .Where(m => m.Id == id)
                .Select(m => new
                {
                    Id = m.Id,
                    ImagePath = m.ImagePath,
                    Manufacturer = new
                    {
                        Id = m.Manu.Id,
                        Name = m.Manu.ManuName // Use ManuName from Manufacturer
                    },
                    MinQty = m.MinQty,
                    ModName = m.ModName,
                    Price = m.Price,
                    SafetyRating = m.SafetyRating,
                    Segment = new
                    {
                        Id = m.Seg.Id,
                        Name = m.Seg.SegName // Use SegName from Segment
                    }
                })
                .FirstOrDefaultAsync();

            return model;
        }
    }
}
