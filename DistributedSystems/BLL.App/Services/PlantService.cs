using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class PlantService : 
    BaseEntityService< BLL.DTO.Plant, Domain.Plant, IPlantRepository>, IPlantService
{

    protected IAppUOW Uow;

    public PlantService(IAppUOW uow, IMapper<BLL.DTO.Plant, Domain.Plant> mapper)
        : base(uow.PlantRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.Plant>> AllAsync(Guid userId)
    {
        return (await Uow.PlantRepository.AllAsync(userId)).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.Plant?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.PlantRepository.FindAsync(id, userId));
    }

    public new async Task<DTO.Plant?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.PlantRepository.RemoveAsync(id, userId));
    }

}