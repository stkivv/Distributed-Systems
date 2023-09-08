using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;

namespace DAL.Repositories;

public class PestSeverityRepository : EFBaseRepository<PestSeverity, ApplicationDbContext>, IPestSeverityRepository
{
    public PestSeverityRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}