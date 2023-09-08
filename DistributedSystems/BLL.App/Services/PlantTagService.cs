using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class PlantTagService : 
    BaseEntityService< BLL.DTO.PlantTag, Domain.PlantTag, IPlantTagRepository>, IPlantTagService
{

    protected IAppUOW Uow;

    public PlantTagService(IAppUOW uow, IMapper<BLL.DTO.PlantTag, Domain.PlantTag> mapper)
        : base(uow.PlantTagRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.PlantTag>> AllAsync()
    {
        return (await Uow.PlantTagRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.PlantTag?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.PlantTagRepository.FindAsync(id));
    }

    public new async Task<DTO.PlantTag?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.PlantTagRepository.RemoveAsync(id));
    }

}