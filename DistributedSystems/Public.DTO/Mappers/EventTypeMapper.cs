using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class EventTypeMapper : BaseMapper<BLL.DTO.EventType, Public.DTO.v1.EventType>
{
    public EventTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}