using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PlantCollectionMapper : BaseMapper<BLL.DTO.PlantCollection, Public.DTO.v1.PlantCollection>
{
    public PlantCollectionMapper(IMapper mapper) : base(mapper)
    {
    }
}