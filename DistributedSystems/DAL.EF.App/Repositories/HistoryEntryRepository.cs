using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Repositories;

public class HistoryEntryRepository : EFBaseRepository<HistoryEntry, ApplicationDbContext>, IHistoryEntryRepository
{
    public HistoryEntryRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<HistoryEntry>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.HistoryEntryType)
            .OrderBy(e => e.EntryTime)
            .ToListAsync();
    }

    public override async Task<HistoryEntry?> FindAsync(Guid id)
    {
        var entry = await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.HistoryEntryType)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();

        return entry;
    }
    
}