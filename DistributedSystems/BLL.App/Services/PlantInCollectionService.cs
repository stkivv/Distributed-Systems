using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class PlantInCollectionService : 
    BaseEntityService< BLL.DTO.PlantInCollection, Domain.PlantInCollection, IPlantInCollectionRepository>, IPlantInCollectionService
{

    protected IAppUOW Uow;

    public PlantInCollectionService(IAppUOW uow, IMapper<BLL.DTO.PlantInCollection, Domain.PlantInCollection> mapper)
        : base(uow.PlantInCollectionRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.PlantInCollection>> AllAsync()
    {
        return (await Uow.PlantInCollectionRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.PlantInCollection?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.PlantInCollectionRepository.FindAsync(id));
    }

    public new async Task<DTO.PlantInCollection?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.PlantInCollectionRepository.RemoveAsync(id));
    }

}