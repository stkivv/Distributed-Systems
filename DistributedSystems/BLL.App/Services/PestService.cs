using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class PestService : 
    BaseEntityService< BLL.DTO.Pest, Domain.Pest, IPestRepository>, IPestService
{

    protected IAppUOW Uow;

    public PestService(IAppUOW uow, IMapper<BLL.DTO.Pest, Domain.Pest> mapper)
        : base(uow.PestRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.Pest>> AllAsync()
    {
        return (await Uow.PestRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.Pest?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.PestRepository.FindAsync(id));
    }

    public new async Task<DTO.Pest?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.PestRepository.RemoveAsync(id));
    }

}