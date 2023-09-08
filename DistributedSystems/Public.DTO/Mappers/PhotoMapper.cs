using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PhotoMapper : BaseMapper<BLL.DTO.Photo, Public.DTO.v1.Photo>
{
    public PhotoMapper(IMapper mapper) : base(mapper)
    {
    }
}