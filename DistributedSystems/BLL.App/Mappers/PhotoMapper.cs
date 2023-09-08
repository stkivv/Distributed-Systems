
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PhotoMapper : BaseMapper<BLL.DTO.Photo, Domain.Photo>
{
    public PhotoMapper(IMapper mapper) : base(mapper)
    {
    }
}