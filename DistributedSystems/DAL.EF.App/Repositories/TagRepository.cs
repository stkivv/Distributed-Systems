using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class TagRepository : EFBaseRepository<Tag, ApplicationDbContext>, ITagRepository
{
    public TagRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public virtual async Task<IEnumerable<Tag>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.TagColor)
            .Where(e => e.AppUserId == userId)
            .ToListAsync();
    }

    public async Task<Tag?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.TagColor)
            .FirstOrDefaultAsync(e => e.Id == id && e.AppUserId == userId);
    }

    public override async Task<Tag?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.TagColor)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    
    public async Task<Tag?> RemoveAsync(Guid id, Guid userId)
    {
        var tag = await FindAsync(id, userId);
        return tag == null ? null : Remove(tag);
    }
}