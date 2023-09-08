using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class ReminderTypeMapper : BaseMapper<BLL.DTO.ReminderType, Public.DTO.v1.ReminderType>
{
    public ReminderTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}