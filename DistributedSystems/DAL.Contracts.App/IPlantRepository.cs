using DAL.Contracts.Base;
using Domain;

namespace DAL.Contracts.App;

public interface IPlantRepository : IBaseRepository<Plant>
{
    public Task<IEnumerable<Plant>> AllAsync(Guid userId);
    
    public Task<Plant?> FindAsync(Guid id, Guid userId);
    
    public Task<Plant?> RemoveAsync(Guid id, Guid userId);


}