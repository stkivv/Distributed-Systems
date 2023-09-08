using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class CollectionTypeService : 
    BaseEntityService< BLL.DTO.CollectionType, Domain.CollectionType, ICollectionTypeRepository>, ICollectionTypeService
{

    protected IAppUOW Uow;

    public CollectionTypeService(IAppUOW uow, IMapper<BLL.DTO.CollectionType, Domain.CollectionType> mapper)
        : base(uow.CollectionTypeRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.CollectionType>> AllAsync()
    {
        return (await Uow.CollectionTypeRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.CollectionType?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.CollectionTypeRepository.FindAsync(id));
    }

    public new async Task<DTO.CollectionType?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.CollectionTypeRepository.RemoveAsync(id));
    }

}