using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class ReminderMapper : BaseMapper<BLL.DTO.Reminder, Public.DTO.v1.Reminder>
{
    public ReminderMapper(IMapper mapper) : base(mapper)
    {
    }
}