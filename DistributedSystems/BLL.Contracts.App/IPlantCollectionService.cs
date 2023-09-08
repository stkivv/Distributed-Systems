using BLL.DTO;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IPlantCollectionService : IBaseRepository<BLL.DTO.PlantCollection>
{
    public Task<IEnumerable<PlantCollection>> AllAsync(Guid userId);
    public Task<PlantCollection?> FindAsync(Guid id, Guid userId);
    
    public Task<PlantCollection?> RemoveAsync(Guid id, Guid userId);
}