using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;

namespace DAL.Repositories;

public class ReminderTypeRepository : EFBaseRepository<ReminderType, ApplicationDbContext>, IReminderTypeRepository
{
    public ReminderTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}