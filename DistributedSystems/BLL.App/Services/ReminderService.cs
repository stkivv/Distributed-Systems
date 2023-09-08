using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class ReminderService : 
    BaseEntityService< BLL.DTO.Reminder, Domain.Reminder, IReminderRepository>, IReminderService
{

    protected IAppUOW Uow;

    public ReminderService(IAppUOW uow, IMapper<BLL.DTO.Reminder, Domain.Reminder> mapper)
        : base(uow.ReminderRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.Reminder>> AllAsync(Guid userId)
    {
        return (await Uow.ReminderRepository.AllAsync(userId)).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.Reminder?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.ReminderRepository.FindAsync(id, userId));
    }

    public new async Task<DTO.Reminder?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.ReminderRepository.RemoveAsync(id, userId));
    }

}