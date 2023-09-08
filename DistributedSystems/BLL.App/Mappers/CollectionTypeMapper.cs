using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class CollectionTypeMapper : BaseMapper<BLL.DTO.CollectionType, Domain.CollectionType>
{
    public CollectionTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}