using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle_Config_DotNet_.Models;

namespace Vehicle_Config_DotNet_.Services
{
    public interface IVehicleDetailService
    {
        Task<List<VehicleDetail>> GetComponentsByModelIdAsync(long modelId);
    }
}
