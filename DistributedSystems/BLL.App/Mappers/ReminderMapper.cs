
using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class ReminderMapper : BaseMapper<BLL.DTO.Reminder, Domain.Reminder>
{
    public ReminderMapper(IMapper mapper) : base(mapper)
    {
    }
}