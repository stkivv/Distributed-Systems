using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;

namespace DAL.Repositories;

public class MonthRepository : EFBaseRepository<Month, ApplicationDbContext>, IMonthRepository
{
    public MonthRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}