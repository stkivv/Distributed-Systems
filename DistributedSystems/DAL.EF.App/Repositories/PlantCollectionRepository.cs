using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PlantCollectionRepository : EFBaseRepository<PlantCollection, ApplicationDbContext>, IPlantCollectionRepository
{
    public PlantCollectionRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<PlantCollection>> AllAsync()
    {
        return await RepositoryDbSet
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<PlantCollection>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .OrderBy(e => e.CollectionName)
            .Where(e => e.AppUserId == userId)
            .ToListAsync();
    }


    public override async Task<PlantCollection?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public virtual async Task<PlantCollection?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }

    public async Task<PlantCollection?> RemoveAsync(Guid id, Guid userId)
    {
        var plantColl = await FindAsync(id, userId);
        return plantColl == null ? null : Remove(plantColl);
    }


}