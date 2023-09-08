using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class ReminderActiveMonthMapper : BaseMapper<BLL.DTO.ReminderActiveMonth, Public.DTO.v1.ReminderActiveMonth>
{
    public ReminderActiveMonthMapper(IMapper mapper) : base(mapper)
    {
    }
}