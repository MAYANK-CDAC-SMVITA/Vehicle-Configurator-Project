using Vehicle_Config_DotNet_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vehicle_Config_DotNet_.Services
{
    public interface IManufacturerService
    {
        // Method to get manufacturers by segmentId
        Task<IEnumerable<Manufacturer>> GetManufacturersBySegmentId(long segmentId);
    }
}
