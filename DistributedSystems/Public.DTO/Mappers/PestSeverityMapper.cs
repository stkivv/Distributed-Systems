using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PestSeverityMapper : BaseMapper<BLL.DTO.PestSeverity, Public.DTO.v1.PestSeverity>
{
    public PestSeverityMapper(IMapper mapper) : base(mapper)
    {
    }
}