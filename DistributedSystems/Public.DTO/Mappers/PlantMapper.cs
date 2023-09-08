using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PlantMapper : BaseMapper<BLL.DTO.Plant, Public.DTO.v1.Plant>
{
    public PlantMapper(IMapper mapper) : base(mapper)
    {
    }
}