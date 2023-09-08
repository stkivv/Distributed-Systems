using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ReminderActiveMonthRepository : EFBaseRepository<ReminderActiveMonth, ApplicationDbContext>, IReminderActiveMonthRepository
{
    public ReminderActiveMonthRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<ReminderActiveMonth>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Month)
            .Include(e => e.Reminder)
            .ToListAsync();
    }
    
    public override async Task<ReminderActiveMonth?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.Month)
            .Include(e => e.Reminder)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}