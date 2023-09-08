using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PlantInCollectionRepository : EFBaseRepository<PlantInCollection, ApplicationDbContext>, IPlantInCollectionRepository
{
    public PlantInCollectionRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<PlantInCollection>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.PlantCollection)
            .ToListAsync();
    }


    public override async Task<PlantInCollection?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .Include(e => e.PlantCollection)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}