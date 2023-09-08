using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;

namespace DAL.Repositories;

public class TagColorRepository : EFBaseRepository<TagColor, ApplicationDbContext>, ITagColorRepository
{
    public TagColorRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}