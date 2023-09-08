using DAL.Contracts.Base;
using Domain;

namespace DAL.Contracts.App;

public interface IPlantCollectionRepository : IBaseRepository<PlantCollection>
{
    public Task<IEnumerable<PlantCollection>> AllAsync(Guid userId);
    public Task<PlantCollection?> FindAsync(Guid id, Guid userId);
    
    public Task<PlantCollection?> RemoveAsync(Guid id, Guid userId);

}