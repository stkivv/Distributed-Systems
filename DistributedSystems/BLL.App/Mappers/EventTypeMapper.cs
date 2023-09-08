using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class EventTypeMapper : BaseMapper<BLL.DTO.EventType, Domain.EventType>
{
    public EventTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}