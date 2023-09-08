using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class HistoryEntryTypeService : 
    BaseEntityService< BLL.DTO.HistoryEntryType, Domain.HistoryEntryType, IHistoryEntryTypeRepository>, IHistoryEntryTypeService
{

    protected IAppUOW Uow;

    public HistoryEntryTypeService(IAppUOW uow, IMapper<BLL.DTO.HistoryEntryType, Domain.HistoryEntryType> mapper)
        : base(uow.HistoryEntryTypeRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.HistoryEntryType>> AllAsync()
    {
        return (await Uow.HistoryEntryTypeRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.HistoryEntryType?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.HistoryEntryTypeRepository.FindAsync(id));
    }

    public new async Task<DTO.HistoryEntryType?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.HistoryEntryTypeRepository.RemoveAsync(id));
    }

}