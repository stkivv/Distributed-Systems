using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class ReminderActiveMonthService : 
    BaseEntityService< BLL.DTO.ReminderActiveMonth, Domain.ReminderActiveMonth, IReminderActiveMonthRepository>, IReminderActiveMonthService
{

    protected IAppUOW Uow;

    public ReminderActiveMonthService(IAppUOW uow, IMapper<BLL.DTO.ReminderActiveMonth, Domain.ReminderActiveMonth> mapper)
        : base(uow.ReminderActiveMonthRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.ReminderActiveMonth>> AllAsync()
    {
        return (await Uow.ReminderActiveMonthRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.ReminderActiveMonth?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.ReminderActiveMonthRepository.FindAsync(id));
    }

    public new async Task<DTO.ReminderActiveMonth?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.ReminderActiveMonthRepository.RemoveAsync(id));
    }

}