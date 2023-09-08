using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class TagColorService : 
    BaseEntityService< BLL.DTO.TagColor, Domain.TagColor, ITagColorRepository>, ITagColorService
{

    protected IAppUOW Uow;

    public TagColorService(IAppUOW uow, IMapper<BLL.DTO.TagColor, Domain.TagColor> mapper)
        : base(uow.TagColorRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.TagColor>> AllAsync()
    {
        return (await Uow.TagColorRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.TagColor?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.TagColorRepository.FindAsync(id));
    }

    public new async Task<DTO.TagColor?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.TagColorRepository.RemoveAsync(id));
    }

}