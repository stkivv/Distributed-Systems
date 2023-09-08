using DAL.Contracts.Base;
using Domain;

namespace DAL.Contracts.App;

public interface ITagRepository : IBaseRepository<Tag>
{
    public Task<IEnumerable<Tag>> AllAsync(Guid userId);
    
    public Task<Tag?> FindAsync(Guid id, Guid userId);
    
    public Task<Tag?> RemoveAsync(Guid id, Guid userId);

}