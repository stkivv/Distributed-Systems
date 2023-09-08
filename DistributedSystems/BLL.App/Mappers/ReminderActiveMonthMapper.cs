
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class ReminderActiveMonthMapper : BaseMapper<BLL.DTO.ReminderActiveMonth, Domain.ReminderActiveMonth>
{
    public ReminderActiveMonthMapper(IMapper mapper) : base(mapper)
    {
    }
}