using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;

namespace DAL.Repositories;

public class HistoryEntryTypeRepository : EFBaseRepository<HistoryEntryType, ApplicationDbContext>, IHistoryEntryTypeRepository
{
    public HistoryEntryTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}