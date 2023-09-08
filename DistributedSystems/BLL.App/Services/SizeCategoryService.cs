using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class SizeCategoryService : 
    BaseEntityService< BLL.DTO.SizeCategory, Domain.SizeCategory, ISizeCategoryRepository>, ISizeCategoryService
{

    protected IAppUOW Uow;

    public SizeCategoryService(IAppUOW uow, IMapper<BLL.DTO.SizeCategory, Domain.SizeCategory> mapper)
        : base(uow.SizeCategoryRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.SizeCategory>> AllAsync()
    {
        return (await Uow.SizeCategoryRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.SizeCategory?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.SizeCategoryRepository.FindAsync(id));
    }

    public new async Task<DTO.SizeCategory?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.SizeCategoryRepository.RemoveAsync(id));
    }

}