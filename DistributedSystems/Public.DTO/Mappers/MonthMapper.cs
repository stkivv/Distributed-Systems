using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class MonthMapper : BaseMapper<BLL.DTO.Month, Public.DTO.v1.Month>
{
    public MonthMapper(IMapper mapper) : base(mapper)
    {
    }
}