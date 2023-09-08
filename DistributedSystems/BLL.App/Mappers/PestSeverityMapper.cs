using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PestSeverityMapper : BaseMapper<BLL.DTO.PestSeverity, Domain.PestSeverity>
{
    public PestSeverityMapper(IMapper mapper) : base(mapper)
    {
    }
}