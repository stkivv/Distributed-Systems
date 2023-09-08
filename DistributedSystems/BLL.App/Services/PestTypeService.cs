using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class PestTypeService : 
    BaseEntityService< BLL.DTO.PestType, Domain.PestType, IPestTypeRepository>, IPestTypeService
{

    protected IAppUOW Uow;

    public PestTypeService(IAppUOW uow, IMapper<BLL.DTO.PestType, Domain.PestType> mapper)
        : base(uow.PestTypeRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.PestType>> AllAsync()
    {
        return (await Uow.PestTypeRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.PestType?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.PestTypeRepository.FindAsync(id));
    }

    public new async Task<DTO.PestType?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.PestTypeRepository.RemoveAsync(id));
    }

}