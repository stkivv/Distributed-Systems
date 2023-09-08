using BLL.DTO;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IReminderService : IBaseRepository<BLL.DTO.Reminder>
{
    public Task<IEnumerable<Reminder>> AllAsync(Guid userId);
    public Task<Reminder?> FindAsync(Guid id, Guid userId);
    
    public Task<Reminder?> RemoveAsync(Guid id, Guid userId);
}