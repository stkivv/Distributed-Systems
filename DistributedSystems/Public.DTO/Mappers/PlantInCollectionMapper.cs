using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PlantInCollectionMapper : BaseMapper<BLL.DTO.PlantInCollection, Public.DTO.v1.PlantInCollection>
{
    public PlantInCollectionMapper(IMapper mapper) : base(mapper)
    {
    }
}