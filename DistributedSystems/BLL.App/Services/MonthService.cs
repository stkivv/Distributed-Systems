using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class MonthService : 
    BaseEntityService< BLL.DTO.Month, Domain.Month, IMonthRepository>, IMonthService
{

    protected IAppUOW Uow;

    public MonthService(IAppUOW uow, IMapper<BLL.DTO.Month, Domain.Month> mapper)
        : base(uow.MonthRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.Month>> AllAsync()
    {
        return (await Uow.MonthRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.Month?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.MonthRepository.FindAsync(id));
    }

    public new async Task<DTO.Month?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.MonthRepository.RemoveAsync(id));
    }

}