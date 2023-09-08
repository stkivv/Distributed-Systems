using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ReminderRepository : EFBaseRepository<Reminder, ApplicationDbContext>, IReminderRepository
{
    public ReminderRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<Reminder>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
                .ThenInclude(e => e!.HistoryEntries)!
                    .ThenInclude(e => e.HistoryEntryType)
                        .ThenInclude(e => e!.EventType)
            .Include(e => e.ReminderType)
                .ThenInclude(e => e!.EventType)
            .Include(e => e.ReminderActiveMonths)!
                .ThenInclude(e => e.Month)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<Reminder>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
                .ThenInclude(e => e!.HistoryEntries)!
                    .ThenInclude(e => e.HistoryEntryType)
                        .ThenInclude(e => e!.EventType)
            .Include(e => e.ReminderType)
                .ThenInclude(e => e!.EventType)
            .Include(e => e.ReminderActiveMonths)!
                .ThenInclude(e => e.Month)
            .Where(e => e.AppUserId == userId)
            .ToListAsync();
    }


    public override async Task<Reminder?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
                .ThenInclude(e => e!.HistoryEntries)!
                    .ThenInclude(e => e.HistoryEntryType)
                        .ThenInclude(e => e!.EventType)
            .Include(e => e.ReminderType)
                .ThenInclude(e => e!.EventType)
            .Include(e => e.ReminderActiveMonths)!
                .ThenInclude(e => e.Month)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public virtual async Task<Reminder?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
                .ThenInclude(e => e!.HistoryEntries)!
                    .ThenInclude(e => e.HistoryEntryType)
                        .ThenInclude(e => e!.EventType)
            .Include(e => e.ReminderType)
                .ThenInclude(e => e!.EventType)
            .Include(e => e.ReminderActiveMonths)!
                .ThenInclude(e => e.Month)
            .Where(e => e.AppUserId == userId)
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }

    public async Task<Reminder?> RemoveAsync(Guid id, Guid userId)
    {
        var reminder = await FindAsync(id, userId);
        return reminder == null ? null : Remove(reminder);
    }
}