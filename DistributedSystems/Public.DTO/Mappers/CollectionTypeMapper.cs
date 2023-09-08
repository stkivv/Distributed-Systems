using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class CollectionTypeMapper : BaseMapper<BLL.DTO.CollectionType, Public.DTO.v1.CollectionType>
{
    public CollectionTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}