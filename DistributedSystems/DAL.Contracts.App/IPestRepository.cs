using DAL.Contracts.Base;
using Domain;

namespace DAL.Contracts.App;

public interface IPestRepository : IBaseRepository<Pest>
{
    public Task<IEnumerable<Pest>> AllAsync(Guid userId, Guid plantId);

}