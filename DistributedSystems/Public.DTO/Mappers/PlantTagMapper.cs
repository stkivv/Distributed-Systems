using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PlantTagMapper : BaseMapper<BLL.DTO.PlantTag, Public.DTO.v1.PlantTag>
{
    public PlantTagMapper(IMapper mapper) : base(mapper)
    {
    }
}