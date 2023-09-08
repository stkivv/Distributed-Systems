using BLL.DTO;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ITagService : IBaseRepository<BLL.DTO.Tag>
{
    public Task<IEnumerable<Tag>> AllAsync(Guid userId);
    public Task<Tag?> FindAsync(Guid id, Guid userId);
    
    public Task<Tag?> RemoveAsync(Guid id, Guid userId);
}