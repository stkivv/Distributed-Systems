using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PlantTagMapper : BaseMapper<BLL.DTO.PlantTag, Domain.PlantTag>
{
    public PlantTagMapper(IMapper mapper) : base(mapper)
    {
    }
}