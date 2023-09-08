using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PhotoRepository : EFBaseRepository<Photo, ApplicationDbContext>, IPhotoRepository
{
    public PhotoRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Photo>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .ToListAsync();
    }

    public override async Task<Photo?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.Plant)
            .FirstOrDefaultAsync();
    }
}