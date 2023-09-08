using DAL.Contracts.Base;
using Domain;

namespace DAL.Contracts.App;

public interface IReminderRepository : IBaseRepository<Reminder>
{
    public Task<IEnumerable<Reminder>> AllAsync(Guid userId);

    public Task<Reminder?> FindAsync(Guid id, Guid userId);
    
    public Task<Reminder?> RemoveAsync(Guid id, Guid userId);


}