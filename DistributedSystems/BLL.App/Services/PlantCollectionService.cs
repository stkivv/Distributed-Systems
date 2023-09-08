using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class PlantCollectionService : 
    BaseEntityService< BLL.DTO.PlantCollection, Domain.PlantCollection, IPlantCollectionRepository>, IPlantCollectionService
{

    protected IAppUOW Uow;

    public PlantCollectionService(IAppUOW uow, IMapper<BLL.DTO.PlantCollection, Domain.PlantCollection> mapper)
        : base(uow.PlantCollectionRepository, mapper)
    {
        Uow = uow;
    }


    public async Task<IEnumerable<DTO.PlantCollection>> AllAsync(Guid userId)
    {
        return (await Uow.PlantCollectionRepository.AllAsync(userId)).Select(e => Mapper.Map(e))!;
    }

    public async Task<DTO.PlantCollection?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.PlantCollectionRepository.FindAsync(id, userId));
    }

    public async Task<DTO.PlantCollection?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.PlantCollectionRepository.RemoveAsync(id, userId));
    }

}