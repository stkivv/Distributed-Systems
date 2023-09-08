using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;

namespace DAL.Repositories;

public class EventTypeRepository : EFBaseRepository<EventType, ApplicationDbContext>, IEventTypeRepository
{
    public EventTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}