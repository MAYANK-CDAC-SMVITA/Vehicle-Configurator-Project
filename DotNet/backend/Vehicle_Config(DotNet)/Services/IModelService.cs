using System.Collections.Generic;
using System.Threading.Tasks;
namespace Vehicle_Config_DotNet_.Services

{
    public interface IModelService
    {
        //Task<IEnumerable<Model>> GetModelsBySegmentAndManufacturer(long segmentId, long manufacturerId);

        public IEnumerable<object> GetModelsBySegmentAndManufacturer(long segmentId, long manufacturerId);

        Task<object> GetModelByIdAsync(long id);
    }
}
