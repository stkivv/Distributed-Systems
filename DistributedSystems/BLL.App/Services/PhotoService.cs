using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.services;

public class PhotoService : 
    BaseEntityService< BLL.DTO.Photo, Domain.Photo, IPhotoRepository>, IPhotoService
{

    protected IAppUOW Uow;

    public PhotoService(IAppUOW uow, IMapper<BLL.DTO.Photo, Domain.Photo> mapper)
        : base(uow.PhotoRepository, mapper)
    {
        Uow = uow;
    }


    public new async Task<IEnumerable<DTO.Photo>> AllAsync()
    {
        return (await Uow.PhotoRepository.AllAsync()).Select(e => Mapper.Map(e))!;
    }

    public new async Task<DTO.Photo?> FindAsync(Guid id)
    {
        return Mapper.Map(await Uow.PhotoRepository.FindAsync(id));
    }

    public new async Task<DTO.Photo?> RemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.PhotoRepository.RemoveAsync(id));
    }

}