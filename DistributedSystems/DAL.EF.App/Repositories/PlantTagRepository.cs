using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PlantTagRepository : EFBaseRepository<PlantTag, ApplicationDbContext>, IPlantTagRepository
{
    public PlantTagRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<PlantTag>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.Tag)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<PlantTag>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.Tag)
            .Where(e => e.Plant!.AppUserId == userId)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<PlantTag>> AllAsync(Guid userId, Guid plantId)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.Tag)
            .Where(e => e.Plant!.AppUserId == userId && e.PlantId == plantId)
            .ToListAsync();
    }

    public override async Task<PlantTag?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.Tag)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}