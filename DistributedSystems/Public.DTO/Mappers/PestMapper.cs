using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PestMapper : BaseMapper<BLL.DTO.Pest, Public.DTO.v1.Pest>
{
    public PestMapper(IMapper mapper) : base(mapper)
    {
    }
}