using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class TagService : 
    BaseEntityService< BLL.DTO.Tag, Domain.Tag, ITagRepository>, ITagService
{

    protected IAppUOW Uow;

    public TagService(IAppUOW uow, IMapper<BLL.DTO.Tag, Domain.Tag> mapper)
        : base(uow.TagRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.Tag>> AllAsync(Guid userId)
    {
        return (await Uow.TagRepository.AllAsync(userId)).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.Tag?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.TagRepository.FindAsync(id, userId));
    }

    public new async Task<DTO.Tag?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.TagRepository.RemoveAsync(id, userId));
    }

}