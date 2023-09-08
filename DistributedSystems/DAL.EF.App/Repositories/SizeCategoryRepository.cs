using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;

namespace DAL.Repositories;

public class SizeCategoryRepository : EFBaseRepository<SizeCategory, ApplicationDbContext>, ISizeCategoryRepository
{
    public SizeCategoryRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}