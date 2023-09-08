using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PlantRepository : EFBaseRepository<Plant, ApplicationDbContext>, IPlantRepository
{
    public PlantRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<Plant>> AllAsync()
    {
        return await RepositoryDbSet
            //.Include(e => e.Pests)
            //.Include(e => e.Reminders)
            //.Include(e => e.HistoryEntries)
            .Include(e => e.Photos)
            .Include(e => e.PlantTags)!
                .ThenInclude(e => e.Tag)
                    .ThenInclude(e => e!.TagColor)
            .Include(e => e.PlantInCollections)!
                .ThenInclude(e => e.PlantCollection)
            .Include(e => e.SizeCategory)
            .OrderBy(e => e.PlantName)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<Plant>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
                //.Include(e => e.Pests)
                //.Include(e => e.Reminders)
                //.Include(e => e.HistoryEntries)
                .Include(e => e.Photos)
                .Include(e => e.PlantTags)!
                    .ThenInclude(e => e.Tag)
                        .ThenInclude(e => e!.TagColor)
                .Include(e => e.PlantInCollections)!
                    .ThenInclude(e => e.PlantCollection)
                .Include(e => e.SizeCategory)
                .OrderBy(e => e.PlantName)
                .Where(e => e.AppUserId == userId)
                .ToListAsync();
    }

    public override async Task<Plant?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.Pests)!
                .ThenInclude(e => e.PestType)
            .Include(e => e.Pests)!
                .ThenInclude(e => e.PestSeverity)
            .Include(e => e.Photos)
            .Include(e => e.Reminders)
            .Include(e => e.HistoryEntries)!
                .ThenInclude(e => e.HistoryEntryType)
            .Include(e => e.PlantTags)!
                .ThenInclude(e => e.Tag)
                    .ThenInclude(e => e!.TagColor)
            .Include(e => e.PlantInCollections)!
                .ThenInclude(e => e.PlantCollection)
            .Include(e => e.SizeCategory)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    public virtual async Task<Plant?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.Pests)!
                .ThenInclude(e => e.PestType)
            .Include(e => e.Pests)!
                .ThenInclude(e => e.PestSeverity)
            .Include(e => e.Photos)
            .Include(e => e.Reminders)
            .Include(e => e.HistoryEntries)!
                .ThenInclude(e => e.HistoryEntryType)
            .Include(e => e.PlantTags)!
                .ThenInclude(e => e.Tag)
                    .ThenInclude(e => e!.TagColor)
            .Include(e => e.PlantInCollections)!
                .ThenInclude(e => e.PlantCollection)
            .Include(e => e.SizeCategory)
            .FirstOrDefaultAsync(e => e.Id == id && e.AppUserId == userId);
    }

    public async Task<Plant?> RemoveAsync(Guid id, Guid userId)
    {
        var plant = await FindAsync(id, userId);
        return plant == null ? null : Remove(plant);
    }
}