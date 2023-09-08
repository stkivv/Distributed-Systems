
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PlantMapper : BaseMapper<BLL.DTO.Plant, Domain.Plant>
{
    public PlantMapper(IMapper mapper) : base(mapper)
    {
    }
}