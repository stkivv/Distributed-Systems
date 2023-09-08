
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PlantInCollectionMapper : BaseMapper<BLL.DTO.PlantInCollection, Domain.PlantInCollection>
{
    public PlantInCollectionMapper(IMapper mapper) : base(mapper)
    {
    }
}