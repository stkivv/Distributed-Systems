using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class ReminderTypeService : 
    BaseEntityService< BLL.DTO.ReminderType, Domain.ReminderType, IReminderTypeRepository>, IReminderTypeService
{

    protected IAppUOW Uow;

    public ReminderTypeService(IAppUOW uow, IMapper<BLL.DTO.ReminderType, Domain.ReminderType> mapper)
        : base(uow.ReminderTypeRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.ReminderType>> AllAsync()
    {
        return (await Uow.ReminderTypeRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.ReminderType?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.ReminderTypeRepository.FindAsync(id));
    }

    public new async Task<DTO.ReminderType?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.ReminderTypeRepository.RemoveAsync(id));
    }

}