using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CollectionTypeRepository : EFBaseRepository<CollectionType, ApplicationDbContext>, ICollectionTypeRepository
{
    public CollectionTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}