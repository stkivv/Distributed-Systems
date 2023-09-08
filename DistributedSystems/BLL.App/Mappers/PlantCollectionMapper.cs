
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PlantCollectionMapper : BaseMapper<BLL.DTO.PlantCollection, Domain.PlantCollection>
{
    public PlantCollectionMapper(IMapper mapper) : base(mapper)
    {
    }
}