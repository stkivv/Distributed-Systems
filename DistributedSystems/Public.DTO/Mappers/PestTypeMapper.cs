using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PestTypeMapper : BaseMapper<BLL.DTO.PestType, Public.DTO.v1.PestType>
{
    public PestTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}