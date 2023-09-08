using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PestRepository : EFBaseRepository<Pest, ApplicationDbContext>, IPestRepository
{
    public PestRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<Pest>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.PestSeverity)
            .Include(e => e.PestType)
            .OrderBy(e => e.PestDiscoveryTime)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<Pest>> AllAsync(Guid userId, Guid plantId)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.PestSeverity)
            .Include(e => e.PestType)
            .Where(e => e.Plant!.AppUserId == userId && e.PlantId == plantId)
            .OrderBy(e => e.PestDiscoveryTime)
            .ToListAsync();
    }
    
    public override async Task<Pest?> FindAsync(Guid id)
    {
        var entry = await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.PestSeverity)
            .Include(e => e.PestType)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();

        return entry;
    }
}