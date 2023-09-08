using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class EventTypeService : 
    BaseEntityService< BLL.DTO.EventType, Domain.EventType, IEventTypeRepository>, IEventTypeService
{

    protected IAppUOW Uow;

    public EventTypeService(IAppUOW uow, IMapper<BLL.DTO.EventType, Domain.EventType> mapper)
        : base(uow.EventTypeRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.EventType>> AllAsync()
    {
        return (await Uow.EventTypeRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.EventType?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.EventTypeRepository.FindAsync(id));
    }

    public new async Task<DTO.EventType?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.EventTypeRepository.RemoveAsync(id));
    }

}