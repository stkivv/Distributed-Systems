using BLL.DTO;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IPlantService  : IBaseRepository<BLL.DTO.Plant>
{
    public Task<IEnumerable<Plant>> AllAsync(Guid userId);
    public Task<Plant?> FindAsync(Guid id, Guid userId);
    
    public Task<Plant?> RemoveAsync(Guid id, Guid userId);
}