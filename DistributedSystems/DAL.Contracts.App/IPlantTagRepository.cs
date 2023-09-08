using DAL.Contracts.Base;
using Domain;

namespace DAL.Contracts.App;

public interface IPlantTagRepository : IBaseRepository<PlantTag>
{
    public Task<IEnumerable<PlantTag>> AllAsync(Guid userId, Guid plantId);
    
    public Task<IEnumerable<PlantTag>> AllAsync(Guid userId);

}