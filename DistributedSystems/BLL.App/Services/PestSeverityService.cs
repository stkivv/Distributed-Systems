using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class PestSeverityService : 
    BaseEntityService< BLL.DTO.PestSeverity, Domain.PestSeverity, IPestSeverityRepository>, IPestSeverityService
{

    protected IAppUOW Uow;

    public PestSeverityService(IAppUOW uow, IMapper<BLL.DTO.PestSeverity, Domain.PestSeverity> mapper)
        : base(uow.PestSeverityRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.PestSeverity>> AllAsync()
    {
        return (await Uow.PestSeverityRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.PestSeverity?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.PestSeverityRepository.FindAsync(id));
    }

    public new async Task<DTO.PestSeverity?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.PestSeverityRepository.RemoveAsync(id));
    }

}