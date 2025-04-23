using Microsoft.EntityFrameworkCore;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;

public class ComponentDetailService
{
    private readonly ProjectContext _context;

    public ComponentDetailService(ProjectContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, object>> GetComponentDetailsByModelIdAsync(long modelId)
    {
        // Fetch and project VehicleDetails
        var vehicleDetails = await _context.VehicleDetails
             .Include(vd => vd.Model)
                 .ThenInclude(m => m.Seg)        // Ensure Segment is loaded
             .Include(vd => vd.Model)
                 .ThenInclude(m => m.Manu)       // Ensure Manufacturer is loaded
             .Include(vd => vd.Comp)
             .Where(vd => vd.ModelId == modelId)
             .Select(vd => new VehicleDetailDto
             {
                 Id = vd.ConfiId, // Adjust property name according to your VehicleDetail POCO
                 CompType = vd.CompType,        // Assuming these properties exist
                 IsConfigurable = vd.IsConfigurable,
                 Model = new ModelDto
                 {
                     Id = vd.Model.Id,
                     ModName = vd.Model.ModName,
                     Price = vd.Model.Price,
                     ImagePath = vd.Model.ImagePath,
                     MinQty = vd.Model.MinQty,
                     SafetyRating = vd.Model.SafetyRating,
                     Segment = new SegmentDto
                     {
                         Id = vd.Model.Seg.Id,
                         Name = vd.Model.Seg.SegName // Adjust if your POCO uses a different property name
                     },
                     Manufacturer = new ManufacturerDto
                     {
                         Id = vd.Model.Manu.Id,
                         Name = vd.Model.Manu.ManuName // Adjust accordingly
                     }
                 },
                 Component = new ComponentDto
                 {
                     Id = vd.Comp.CompId,
                     ComponentName = vd.Comp.CompName
                 }
             })
             .ToListAsync();

        // Fetch and project AltComponents
        var altComponents = await _context.AltComponents
             .Include(ac => ac.Model)
                 .ThenInclude(m => m.Seg)
             .Include(ac => ac.Model)
                 .ThenInclude(m => m.Manu)
             .Include(ac => ac.Comp)
             .Include(ac => ac.AltComp)
             .Where(ac => ac.ModelId == modelId)
             .Select(ac => new AltComponentDto
             {
                 Id = ac.AltId,
                 PriceVariation = ac.PriceVariation,
                 Model = new ModelDto
                 {
                     Id = ac.Model.Id,
                     ModName = ac.Model.ModName,
                     Price = ac.Model.Price,
                     ImagePath = ac.Model.ImagePath,
                     MinQty = ac.Model.MinQty,
                     SafetyRating = ac.Model.SafetyRating,
                     Segment = new SegmentDto
                     {
                         Id = ac.Model.Seg.Id,
                         Name = ac.Model.Seg.SegName,
                     },
                     Manufacturer = new ManufacturerDto
                     {
                         Id = ac.Model.Manu.Id,
                         Name = ac.Model.Manu.ManuName
                     }
                 },
                 Component = new ComponentDto
                 {
                     Id = ac.Comp.CompId,
                     ComponentName = ac.Comp.CompName
                 },
                 AltComponent = new ComponentDto
                 {
                     Id = ac.AltComp.CompId,
                     ComponentName = ac.AltComp.CompName
                 }
             })
             .ToListAsync();

        var response = new Dictionary<string, object>
    {
         { "vehicleComponents", vehicleDetails },
         { "alternateComponents", altComponents }
    };

        return response;
    }



}
