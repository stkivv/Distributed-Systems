using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;

namespace DAL.Repositories;

public class PestTypeRepository : EFBaseRepository<PestType, ApplicationDbContext>, IPestTypeRepository
{
    public PestTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}