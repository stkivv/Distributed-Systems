using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class HistoryEntryService : 
    BaseEntityService< BLL.DTO.HistoryEntry, Domain.HistoryEntry, IHistoryEntryRepository>, IHistoryEntryService
{

    protected IAppUOW Uow;

    public HistoryEntryService(IAppUOW uow, IMapper<BLL.DTO.HistoryEntry, Domain.HistoryEntry> mapper)
        : base(uow.HistoryEntryRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.HistoryEntry>> AllAsync()
    {
        return (await Uow.HistoryEntryRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.HistoryEntry?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.HistoryEntryRepository.FindAsync(id));
    }

    public new async Task<DTO.HistoryEntry?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.HistoryEntryRepository.RemoveAsync(id));
    }

}